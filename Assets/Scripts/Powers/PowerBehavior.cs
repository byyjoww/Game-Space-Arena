using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerBehavior : ScriptableObject
{
    public abstract void RunScript(GameObject prefab, Transform location);
}
