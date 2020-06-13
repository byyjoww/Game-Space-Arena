using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Profile))]
public sealed class Database : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private static Database _i;
    public static Database i
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<Database>("Database"));
            }

            return _i;
        }
    }

    //------------------Profiles------------------//
    [Header("Profiles")]
    public Profile profile;

    private void OnValidate()
    {
        if (profile == null) profile = GetComponent<Profile>();
    }

    //------------------Item Databases------------------//
    [Header("Databases")]
    [SerializeField] public List<ScriptableDatabase> databases = new List<ScriptableDatabase>();
    public List<ScriptableDatabase> Databases => databases;
}
