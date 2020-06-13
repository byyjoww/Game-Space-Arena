using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Linq;

public class Shop : MonoBehaviour
{
    //--------------------------------------------------------------------// 
    //---EDIT the "ShopSections" ENUM TO INCLUDE ALL BUYABLE ITEM TYPES---//
    //--------------------------------------------------------------------//
    public enum ShopSections { Ship = 0, Power = 1, Coins = 2, Gems = 3, Currencies = 4 };

    //--------------------------------------------------------------------// 
    //------------------------DONT EDIT BELOW THIS------------------------//
    //--------------------------------------------------------------------//

    private Database MyDatabase => Database.i;

    public enum CostType { softCurrency = 0, hardCurrency = 1, prestige = 2, money = 3 }
    [SerializeField] private List<ShopSections> shopSections = new List<ShopSections>();

    //----------UI References----------//
    [SerializeField] private GameObject pfShopSection, pfShopViewPort, pfShopElement;
    [SerializeField] private Transform tShopSection, tShopViewport;    

    //----------Instantiated Objects----------//
    private List<GameObject> activeSections = new List<GameObject>();
    private List<GameObject> activeViewports = new List<GameObject>();
    private List<GameObject> activeElements = new List<GameObject>();

    private void Start()
    {
        CreateShopFromList();
        Profile.OnItemChanged += RefreshShop;
        SaveHandler.OnLoadComplete += RefreshShop;
    }

    private void OnDestroy()
    {
        Profile.OnItemChanged -= RefreshShop;
        SaveHandler.OnLoadComplete -= RefreshShop;
    }

    private void RefreshShop()
    {
        foreach (GameObject g in activeElements)
        {
            Destroy(g);
        }

        activeElements.Clear();

        foreach (ShopSections s in shopSections)
        {
            InstantiateElements(s);
        }

        PurchaseHandler.PurchaseValidator(activeElements);
    }

    private void CreateShopFromList()
    {
        foreach (ShopSections s in shopSections)
        {
            InstantiateViewports(s);
            InstantiateSections(s);
            InstantiateElements(s);
        }

        SelectFirstSection();

        PurchaseHandler.PurchaseValidator(activeElements);
    }

    private void SelectFirstSection()
    {
        activeSections[0].GetComponent<Button>().onClick.Invoke();
    }

    private void InstantiateViewports(ShopSections s)
    {
        GameObject g = Instantiate(pfShopViewPort, tShopViewport);
        g.name = s.ToString();
        activeViewports.Add(g);
    }

    private void InstantiateSections(ShopSections s)
    {
        GameObject pfSection = Instantiate(pfShopSection, tShopSection);
        pfSection.name = s.ToString();
        SetupShopSection setup = pfSection.GetComponent<SetupShopSection>();
        setup.Setup(s.ToString());
        setup.ButtonComponent.onClick.AddListener(delegate { ToggleViewport(setup.ButtonComponent); });
        activeSections.Add(pfSection);
    }

    private void ToggleViewport(Button sectionButtonComponent)
    {
        foreach(GameObject section in activeSections)
        {
            section.GetComponent<Button>().interactable = true;
        }

        foreach(GameObject viewport in activeViewports)
        {
            if (viewport.name != sectionButtonComponent.gameObject.name)
            {
                viewport.SetActive(false);
            }
            else
            {
                viewport.SetActive(true);                
            }

            sectionButtonComponent.interactable = false;
        }
    }

    private void InstantiateElements(ShopSections s)
    {
        ScriptableDatabase database = MyDatabase.Databases.First(x => x.sectionType == s);

        foreach(GameObject v in activeViewports)
        {
            if (v.name == s.ToString())
            {
                foreach (ScriptableElement databaseElement in database.databaseElements)
                {
                    GameObject g = Instantiate(pfShopElement, v.transform.GetChild(0));
                    g.name = databaseElement.itemName;
                    SetupShopItem setup = g.GetComponent<SetupShopItem>();
                    setup.Setup(databaseElement);
                    setup.ButtonComponent.onClick.AddListener(delegate { PurchaseHandler.Purchase(databaseElement); });
                    activeElements.Add(g);
                }                
            }
        }
    }
}