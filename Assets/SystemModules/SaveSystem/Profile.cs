using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Profile : MonoBehaviour
{
    private void Start()
    {
        Initiate();
        OnItemChanged += Save;
    }

    private void OnDestroy()
    {
        Save();
        OnItemChanged -= Save;
    }

    public static event Action OnItemChanged;

    [SerializeField] private List<SaveHandler.SaveableItem> profileItemList;

    public bool CheckForItem(string itemName)
    {
        if (CheckItemAmount(itemName) > 0) return true;
        else return false;
    }

    public int CheckItemAmount(string itemName)
    {
        int saveableAmount = 0;

        foreach (var i in profileItemList)
        {
            if (itemName == i.saveableName)
            {
                saveableAmount = i.saveableAmount;
            }
        }

        return saveableAmount;
    }
    
    public void GetItem(string itemName, int amountToGet)
    {
        foreach (var i in profileItemList)
        {
            if (itemName == i.saveableName)
            {
                i.saveableAmount += amountToGet;
                OnItemChanged?.Invoke();
                Debug.Log(i.saveableName + " has been obtained.");
            }
        }
    }

    public void LoseItem(string itemName, int amountToLose)
    {
        foreach (var i in profileItemList)
        {
            if (itemName == i.saveableName)
            {
                i.saveableAmount -= amountToLose;
                OnItemChanged?.Invoke();
                Debug.Log(i.saveableName + " has been lost.");
            }
        }
    }

    private void Initiate()
    {
        profileItemList = new List<SaveHandler.SaveableItem>();

        SaveHandler.Initiate(profileItemList);
    }

    public void InitiateDebugger()
    {
        Initiate();
        Debug.LogError("Initiated Via Debugger!");
        OnItemChanged?.Invoke();
    }

    private void Save()
    {
        SaveHandler.Save(profileItemList);
        Debug.Log("Saving");
    }
}
