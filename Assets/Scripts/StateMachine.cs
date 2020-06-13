using UnityEngine;
using TMPro;
using System;

public class StateMachine : MonoBehaviour
{
    public static event Action OnGameEnd;

    [SerializeField] private GameObject LoseUI;
    [SerializeField] private bool isGameEnded = false;

    private void GameOver()
    {
        LoseUI.SetActive(true);
        isGameEnded = true;
        OnGameEnd?.Invoke();
        Debug.Log("Game Over");
        //play animation where player blows up        
    }

    public void DeathCheck()
    {
        if (isGameEnded)
        {
            return;
        }
            
        GameOver();
    }

    private void OnDestroy()
    {
        isGameEnded = false;
    }
}