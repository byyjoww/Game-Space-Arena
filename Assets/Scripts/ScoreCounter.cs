using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int enemiesKilled;
    [SerializeField] private int scoreValue;
    public int ScoreValue => scoreValue;

    private void Update()
    {
        scoreText.text = GetScore().ToString();
    }

    public void AddEnemyKilledCount()
    {
        enemiesKilled += 1;
        scoreValue += 10;
    }

    public int GetScore()
    {
        return enemiesKilled * 10;
    }
}
