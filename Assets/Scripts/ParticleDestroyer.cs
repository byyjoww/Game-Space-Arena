using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    [SerializeField] float destroytime = 7f;

    private void Start()
    {
        Destroy(this.gameObject, destroytime);
    }
}
