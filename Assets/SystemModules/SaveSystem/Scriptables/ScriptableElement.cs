using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ScriptableElement", menuName = "ScriptableElement")]
public class ScriptableElement : ScriptableObject
{
    public string itemName = "default Name";
    public Sprite itemSprite = null;
    public Shop.CostType costType = default;
    public float itemCost = 0f;
}