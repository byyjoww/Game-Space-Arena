using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

static class Tools
{
    public static IEnumerator DelayedExecution(Action action, Func<bool> when)
    {
        yield return new WaitUntil(when);
        action.Invoke();
    }

    public static IEnumerator DelayedExecution(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }

    public static void Invoke(MonoBehaviour worker, Action action, float delay = 0)
    {
        worker.StartCoroutine(DelayedExecution(action, delay));
    }

    public static void Invoke(MonoBehaviour worker, Action action, Func<bool> when)
    {
        worker.StartCoroutine(DelayedExecution(action, when));
    }

    public static List<T> GetComponentsInBrothers<T>(this MonoBehaviour obj) where T : MonoBehaviour
    {
        if (obj.transform == null || obj.transform.parent == null)
            return new List<T>();

        List<T> components = new List<T>();
        foreach(Transform child in obj.transform.parent)
        {
            components.AddRange(child.GetComponents<T>());
        }
        return components;
    }
    public static T GetComponentInBrothers<T>(this MonoBehaviour obj) where T : MonoBehaviour
    {
        List<T> components = obj.GetComponentsInBrothers<T>();
        return components.Count > 0 ? components.Single() : null;
    }
    public static T[] GetAllScriptableObjects<T>() where T : ScriptableObject
    {
    #if UNITY_EDITOR
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;
    #endif
        throw new Exception("Script not callable from outside Unity Editor!");
    }
    public static void ValidateComponent<T>(this MonoBehaviour getFrom, ref T field, bool searchInChildren=false) where T : Component
    {
        if (field == null)
        {
            field = searchInChildren ? getFrom.GetComponentInChildren<T>() : getFrom.GetComponent<T>();
            Debug.Log("Auto-getting " + typeof(T) + " component from " + getFrom, getFrom);
            if(field == null)
                Debug.LogWarning("Auto-getter could not find component of type " + typeof(T) + " from " + getFrom, getFrom);
        }
    }

    public static bool Approximately(this Vector3 u, Vector3 v, float eps = 0)
    {
        eps = Mathf.Max(eps, 0) + (float)1E-20;
        return Mathf.Abs((u - v).magnitude) <= eps;
    }
}

public static class LinqExtentions
{
    public static T ArgMin<T>(this IEnumerable<T> list, Func<T, T, int> CompareTo)
    {
        if (list.Count() == 0)
            return default(T);

        T min = list.First();

        foreach (T element in list)
        {
            if (CompareTo(element, min) < 0)
                min = element;
        }
        return min;
    }
    public static T ArgMin<T>(this IEnumerable<T> list, Func<T, int> Kernel)
    {
        return ArgMin(list, (a, b) => Kernel(a) - Kernel(b));
    }
    public static T ArgMax<T>(this IEnumerable<T> list, Func<T, T, int> CompareTo)
    {
        if (list.Count() == 0)
            return default(T);

        T min = list.First();

        foreach (T element in list)
        {
            if (CompareTo(element, min) > 0)
                min = element;
        }
        return min;
    }
    public static T ArgMax<T>(this IEnumerable<T> list, Func<T, int> Kernel)
    {
        return ArgMax(list, (a, b) => Kernel(a) - Kernel(b));
    }

    public static T Random<T>(this IEnumerable<T> list)
    {
        List<T> l = list.ToList();
        int index = UnityEngine.Random.Range(0, l.Count);
        return l[index];
    }

    public static Dictionary<TKey, TValue> ToDictionaryUnique<TKey, TValue>(this IEnumerable<TValue> container, Func<TValue, TKey> keySelector)
    {
        Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
        foreach (TValue s in container)
        {
            if (!dict.ContainsKey(keySelector(s)))
            {
                dict.Add(keySelector(s), s);
            }
            else if(!dict[keySelector(s)].Equals(s))
            {
                Debug.LogWarning("A value with [Key: " + keySelector(s) + "] is already present in dictionary. Cannot add [Value:" + s + "].");
            }            
        }
        return dict;
    }
}

public static class AssemblyTools
{
    public static IEnumerable<Type> GetSubclasses<T>()
    {
        return GetSubclasses(typeof(T));
    }
    public static IEnumerable<Type> GetSubclasses(this Type inheritingFrom)
    {
        Type[] allTypesInAssembly = Assembly.GetAssembly(inheritingFrom).GetTypes();
        // Filter assembly classes: get subclasses of type and remove abstract classes
        return allTypesInAssembly.Where(type => type.IsClass && type.IsSubclassOf(inheritingFrom) && !type.IsAbstract);
    }

    public static MethodInfo GetMethod<TReturn>(Type type)
    {
        // Get the properties of the class but include inherited properties.
        List<PropertyInfo> found = (new Type[] { type }).Concat(type.GetInterfaces()).SelectMany(i => i.GetProperties()).
            Where(m => m.PropertyType == typeof(TReturn)).ToList();
        
        if (found.Count == 1)
        {
            return found.Single().GetMethod;
        }
        else
        {
            Debug.LogError("Type [" + type + "] contains " + found.Count + " Methods / Properties of return type [" + typeof(TReturn).Name + "].");
            return null;
        }
    }
    /**
     * Creates a dictionary from a list of types, mapping a singular enum to a class, using reflection.
     */
    public static Dictionary<En, Type> BuildDictionaryFromEnumProperty<En>(IEnumerable<Type> types) where En : Enum
    {        
        var keyValuePairs = new Dictionary<En, Type>();

        foreach (Type type in types)
        {
            MethodInfo property = GetMethod<En>(type);

            if (property != null)
            {
                En key = (En)property.Invoke(Activator.CreateInstance(type), null);

                if (!keyValuePairs.ContainsKey(key))
                {
                    keyValuePairs.Add(key, type);
                }
                else if(!keyValuePairs[key].Equals(type))
                {
                    throw new Exception("Trying to add duplicated entries to dictionary: [Class " + keyValuePairs[key] + " and " + type + "] both have Enum [" + key + "].");
                }
            }
        }

        return keyValuePairs;
    }

    public static string TypeToString(Type t)
    {
        return t.AssemblyQualifiedName;
    }
    public static Type StringToType(string s)
    {
        return Type.GetType(s);
    }


}
