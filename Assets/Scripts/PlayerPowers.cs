using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowers : MonoBehaviour
{
    [SerializeField] private ScriptablePower[] Inventory;
    [SerializeField] private Image inventorySlot1;
    [SerializeField] private Image inventorySlot2;
    [SerializeField] private Image inventorySlot3;
    [SerializeField] private Image inventorySlot4;
    public int powersUsed;

    private void Start()
    {
        //Initializing Inventory
        Inventory = new ScriptablePower[4];
        Inventory[0] = null;
        Inventory[1] = null;
        Inventory[2] = null;
        Inventory[3] = null;

        //Getting UI Elements
        inventorySlot1 = GameObject.Find("Inv1").GetComponent<Image>();
        inventorySlot2 = GameObject.Find("Inv2").GetComponent<Image>();
        inventorySlot3 = GameObject.Find("Inv3").GetComponent<Image>();
        inventorySlot4 = GameObject.Find("Inv4").GetComponent<Image>();
    }

    private void AddImage(int inventoryNumber, Sprite sprite)
    {
        if (inventoryNumber == 1)
        {
            inventorySlot1.sprite = sprite;
            var tempColor = inventorySlot1.color;
            tempColor.a = 1f;
            inventorySlot1.color = tempColor;
        }
        if (inventoryNumber == 2)
        {
            inventorySlot2.sprite = sprite;
            var tempColor = inventorySlot2.color;
            tempColor.a = 1f;
            inventorySlot2.color = tempColor;
        }            
        if (inventoryNumber == 3)
        {
            inventorySlot3.sprite = sprite;
            var tempColor = inventorySlot3.color;
            tempColor.a = 1f;
            inventorySlot3.color = tempColor;
        }
        if (inventoryNumber == 4)
        {
            inventorySlot4.sprite = sprite;
            var tempColor = inventorySlot4.color;
            tempColor.a = 1f;
            inventorySlot4.color = tempColor;
        }
    }

    private void RemoveImage(int inventoryNumber)
    {
        if (inventoryNumber == 1)
        {
            inventorySlot1.sprite = null;
            var tempColor = inventorySlot1.color;
            tempColor.a = 0f;
            inventorySlot1.color = tempColor;
        }
        if (inventoryNumber == 2)
        {
            inventorySlot2.sprite = null;
            var tempColor = inventorySlot2.color;
            tempColor.a = 0f;
            inventorySlot2.color = tempColor;
        }
        if (inventoryNumber == 3)
        {
            inventorySlot3.sprite = null;
            var tempColor = inventorySlot3.color;
            tempColor.a = 0f;
            inventorySlot3.color = tempColor;
        }
        if (inventoryNumber == 4)
        {
            inventorySlot4.sprite = null;
            var tempColor = inventorySlot4.color;
            tempColor.a = 0f;
            inventorySlot4.color = tempColor;
        }
    }

    public void AddPower(ScriptablePower power)
    {
        if (Inventory[0] == null)
        {
            Inventory[0] = power;
            Sprite s = power.itemSprite;
            Debug.Log(Inventory[0]);
            AddImage(1, s);
        }

        else if (Inventory[1] == null)
        {
            Inventory[1] = power;
            Sprite s = power.itemSprite;
            Debug.Log(Inventory[1]);
            AddImage(2, s);
        }

        else if (Inventory[2] == null)
        {
            Inventory[2] = power;
            Sprite s = power.itemSprite;
            Debug.Log(Inventory[2]);
            AddImage(3, s);
        }

        else if (Inventory[3] == null)
        {
            Inventory[3] = power;
            Sprite s = power.itemSprite;
            Debug.Log(Inventory[3]);
            AddImage(4, s);
        }

        else Debug.Log("Inventory is Full!");
    }

    public void UsePower1()
    {
        if (Inventory[0] != null)
        {
            powersUsed += 1;
            RemoveImage(1);
            Inventory[0].power.RunScript(Inventory[0].itemPrefab, this.transform);
            Inventory[0] = null;
            Debug.Log("Power in Inventory Slot 1 Was used.");
        }
        else Debug.Log("Inventory Slot is empty!");
    }

    public void UsePower2()
    {
        if (Inventory[1] != null)
        {
            powersUsed += 1;
            RemoveImage(2);
            Inventory[1].power.RunScript(Inventory[1].itemPrefab, this.transform);
            Inventory[1] = null;
            Debug.Log("Power in Inventory Slot 2 Was used.");
        }
        else Debug.Log("Inventory Slot is empty!");
    }

    public void UsePower3()
    {
        if (Inventory[2] != null)
        {
            powersUsed += 1;
            RemoveImage(3);
            Inventory[2].power.RunScript(Inventory[2].itemPrefab, this.transform);
            Inventory[2] = null;
            Debug.Log("Power in Inventory Slot 3 Was used.");
        }
        else Debug.Log("Inventory Slot is empty!");
    }

    public void UsePower4()
    {
        if (Inventory[3] != null)
        {
            powersUsed += 1;
            RemoveImage(4);
            Inventory[3].power.RunScript(Inventory[3].itemPrefab, this.transform);
            Inventory[3] = null;
            Debug.Log("Power in Inventory Slot 4 Was used.");
        }
        else Debug.Log("Inventory Slot is empty!");
    }
}
