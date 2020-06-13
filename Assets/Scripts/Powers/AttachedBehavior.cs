using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttachedBehavior", menuName = "AttachedBehavior")]
public class AttachedBehavior : PowerBehavior
{
    public override void RunScript(GameObject prefab, Transform location)
    {
        Instantiate(prefab, location);
    }
}