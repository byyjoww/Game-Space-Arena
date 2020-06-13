using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DetachedBehavior", menuName = "DetachedBehavior")]
public class DetachedBehavior : PowerBehavior
{
    public override void RunScript(GameObject prefab, Transform location)
    {
        Instantiate(prefab, location.position, Quaternion.identity);
    }
}