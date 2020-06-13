using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootHandler : MonoBehaviour
{
    private void Start()
    {
        StateMachine.OnGameEnd += Initialize;
    }

    private void Initialize()
    {
        
    }
}
