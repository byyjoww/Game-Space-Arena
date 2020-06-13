using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public void GetCurrencies()
    {
        Database.i.profile.GetItem("hardCurrency", 1000);
        Database.i.profile.GetItem("softCurrency", 1000);
    }

    public void ResetCurrencies()
    {
        Database.i.profile.LoseItem("hardCurrency", 10000000);
        Database.i.profile.LoseItem("softCurrency", 10000000);
    }

    public void ResetItems()
    {
        PlayerPrefs.DeleteAll();
        Database.i.profile.InitiateDebugger();
    }

    public void SelectSpaceship()
    {
        PlayerSpawner playerSpawner = FindObjectOfType<PlayerSpawner>();
        playerSpawner.DEBUGSelectSpaceship(Database.i.Databases[1].databaseElements[0] as ScriptableShip);
    }
}
