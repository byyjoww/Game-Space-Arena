using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTextComponent;
    [SerializeField] private TMP_Text goldValueTextComponent;
    [SerializeField] private Image medalImageComponent;
    [SerializeField] private ScoreCounter scoreCounter;

    [SerializeField] private Sprite bronzeMedal, silverMedal, goldMedal;

    private void Awake()
    {
        StateMachine.OnGameEnd += Initiate;
    }

    private void OnDestroy()
    {
        StateMachine.OnGameEnd -= Initiate;
    }

    private void Initiate()
    {
        SetScoreValues();
        SetPlayerPrefs();
    }

    private void SetPlayerPrefs()
    {
        if (scoreCounter.ScoreValue > PlayerPrefs.GetInt("Highscore1", 0))
        {
            int i1 = PlayerPrefs.GetInt("Highscore1", 0);
            int i2 = PlayerPrefs.GetInt("Highscore2", 0);
            PlayerPrefs.SetInt("Highscore1", scoreCounter.ScoreValue);
            PlayerPrefs.SetInt("Highscore2", i1);
            PlayerPrefs.SetInt("Highscore3", i2);
        }
        else if (scoreCounter.ScoreValue > PlayerPrefs.GetInt("Highscore2", 0))
        {
            int i2 = PlayerPrefs.GetInt("Highscore2", 0);
            PlayerPrefs.SetInt("Highscore2", scoreCounter.ScoreValue);
            PlayerPrefs.SetInt("Highscore3", i2);
        }
        else if (scoreCounter.ScoreValue > PlayerPrefs.GetInt("Highscore3", 0))
        {
            PlayerPrefs.SetInt("Highscore3", scoreCounter.ScoreValue);
        }
        else Debug.Log("Highscore is lower than top3");

        PlayerPrefs.Save();
    }

    private void SetScoreValues()
    {
        float totalTimeSurvived = FindObjectOfType<MasterTimer>().GetTime();
        int totalTimeSurvivedRounded = Mathf.RoundToInt(totalTimeSurvived);
        scoreTextComponent.text = scoreCounter.ScoreValue.ToString();
        goldValueTextComponent.text = "+ " + scoreCounter.ScoreValue + " Gold";
        SetMedal(scoreCounter.ScoreValue);
        GainGold();
    }

    private void SetMedal(int score)
    {
        if (score < 100)
        {
            medalImageComponent.sprite = bronzeMedal;
        }
        else if (score > 101 && score < 1000)
        {
            medalImageComponent.sprite = silverMedal;
        }
        else
        {
            medalImageComponent.sprite = goldMedal;
        }
    }

    private void GainGold()
    {
        Database.i.profile.GetItem("softCurrency", scoreCounter.ScoreValue);
    }
}
