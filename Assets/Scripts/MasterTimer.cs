using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterTimer : MonoBehaviour
{
    //Game Master Timer
    [SerializeField] private float timeSurvived;

    private void Start()
    {
        StateMachine.OnGameEnd += GameOver;
    }

    private void OnDestroy()
    {
        StateMachine.OnGameEnd -= GameOver;
    }

    void Update()
    {
        timeSurvived += Time.deltaTime;
    }

    public float GetTime()
    {
        return timeSurvived;
    }

    public void GameOver()
    {
        enabled = false;
    }
}
