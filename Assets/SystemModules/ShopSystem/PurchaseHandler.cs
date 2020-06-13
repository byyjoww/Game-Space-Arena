using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PurchaseHandler
{
    public static void PurchaseValidator(List<GameObject> listToValidate)
    {
        var profile = Database.i.profile;

        foreach (GameObject element in listToValidate)
        {
            if (profile.CheckForItem(element.name) == true)
            {
                SetupShopItem setup = element.GetComponent<SetupShopItem>();
                setup.ToggleSoldFlag(true);
                setup.ButtonComponent.interactable = false;
            }
        }
    }

    public static Action CheckPurchaseType(ScriptableElement itemToBuy)
    {
        if (itemToBuy.costType == Shop.CostType.money)
        {
            Debug.Log("IAP");
            return () => PurchaseIAP(itemToBuy);
        }
        else
        {
            Debug.Log("Currency Purchase");
            return () => PurchaseCurrency(itemToBuy);
        }
    }

    public static void Purchase(ScriptableElement itemToBuy)
    {
        Action action = CheckPurchaseType(itemToBuy);
        try
        {
            action?.Invoke();
        }
        catch
        {
            Debug.LogError("Action is empty");
        }
    }

    public static void PurchaseIAP(ScriptableElement itemToBuy)
    {
        //trigger IAP
    }

    public static void DeliverIAP(ScriptableIAP itemToBuy)
    {
        var profile = Database.i.profile;
        profile.GetItem(itemToBuy.itemToGet.itemName, itemToBuy.currenciesToGive);
    }

    public static void PurchaseCurrency(ScriptableElement itemToBuy)
    {
        var profile = Database.i.profile;

        if (profile.CheckForItem(itemToBuy.itemName) == false)
        {
            if (profile.CheckItemAmount(itemToBuy.costType.ToString()) >= (int)itemToBuy.itemCost)
            {
                profile.GetItem(itemToBuy.itemName, 1);
                profile.LoseItem(itemToBuy.costType.ToString(), (int)itemToBuy.itemCost);
            }
            else
            {
                Debug.LogError("Insufficient Currency");
            }
        }
        else
        {
            Debug.LogError("Already Owns Item");
        }
    }

    public static void Sell(ScriptableElement itemToSell)
    {
        var profile = Database.i.profile;

        if (profile.CheckForItem(itemToSell.itemName) == true)
        {
            profile.LoseItem(itemToSell.itemName, 1);
            profile.GetItem(itemToSell.costType.ToString(), (int)itemToSell.itemCost);
        }
        else
        {
            Debug.LogError("Insufficient Items");
        }
    }
}
