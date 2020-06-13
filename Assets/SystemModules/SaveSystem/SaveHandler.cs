using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SaveHandler
{
    public static event Action OnSaveComplete;
    public static event Action OnLoadComplete;

    [System.Serializable] public class SaveableItem
    {
        public string saveableName; //player prefs key
        public int saveableAmount; // amount of items

        public SaveableItem(string name, int purchasedBool)
        {
            this.saveableName = name;
            this.saveableAmount = purchasedBool;
        }
    }

    public static void Initiate(List<SaveableItem> listOfItemsToLoad)
    {
        List<ScriptableItem> allDatabaseItems = FetchAllDatabaseItems();

        foreach (ScriptableItem item in allDatabaseItems)
        {
            if (item.defaultAmount > 0)
            {
                listOfItemsToLoad.Add(new SaveableItem(item.itemName, item.defaultAmount));
            }
            else
            {
                listOfItemsToLoad.Add(new SaveableItem(item.itemName, 0));
            }
        }

        Load(listOfItemsToLoad);
    }

    private static List<ScriptableItem> FetchAllDatabaseItems()
    {
        List<ScriptableItem> allDatabaseItems = new List<ScriptableItem>();

        foreach (ScriptableDatabase database in Database.i.Databases)
        {
            List<ScriptableElement> tempElementList = database.databaseElements;

            foreach (ScriptableElement item in tempElementList.Where(x => x is ScriptableItem))
            {
                allDatabaseItems.Add(item as ScriptableItem);
            }            
        }

        return allDatabaseItems;
    }

    public static void Save(List<SaveableItem> listOfItemsToSave)
    {
        SaveItems(listOfItemsToSave);
        OnSaveComplete?.Invoke();
    }

    public static void SaveItems(List<SaveableItem> listOfItemsToSave)
    {
        foreach (var item in listOfItemsToSave)
        {
            PlayerPrefs.SetInt(item.saveableName, item.saveableAmount);            
        }

        PlayerPrefs.Save();
    }

    public static void Load(List<SaveableItem> listOfItemsToLoad)
    {
        LoadItems(listOfItemsToLoad);
        OnLoadComplete?.Invoke();
    }

    public static void LoadItems(List<SaveableItem> listOfItemsToLoad)
    {
        for (int i = 0; i < listOfItemsToLoad.Count; i++)
        {
            if (listOfItemsToLoad[i].saveableAmount == 1)
            {
                listOfItemsToLoad[i].saveableAmount = PlayerPrefs.GetInt(listOfItemsToLoad[i].saveableName, 1);
            }
            else
            {
                listOfItemsToLoad[i].saveableAmount = PlayerPrefs.GetInt(listOfItemsToLoad[i].saveableName, 0);
            }
        }
    }
}