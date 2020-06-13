using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestruction : MonoBehaviour
{
    [SerializeField] private float destructionTimer = 1f;

    void Start()
    {
        Destroy(this.gameObject, destructionTimer);
    }
}
