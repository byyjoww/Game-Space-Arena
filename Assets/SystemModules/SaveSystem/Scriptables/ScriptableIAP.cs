using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ScriptableIAP", menuName = "ScriptableIAP")]
public class ScriptableIAP : ScriptableElement
{
    public ScriptableElement itemToGet = default;
    public int currenciesToGive = 0;
}