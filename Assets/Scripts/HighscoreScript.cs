using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreScript : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private bool hidePointText;

    void Start()
    {        
        SetText(GetScore(index));
    }

    private int GetScore(int index)
    {
        return PlayerPrefs.GetInt("Highscore" + index, 0);
    }

    private void SetText(int highscoreValue)
    {
        if (hidePointText == true)
        {
            textComponent.text = highscoreValue.ToString();
        }
        else
        {
            textComponent.text = highscoreValue.ToString() + " points";
        }        
    }

    private void OnValidate()
    {
        if (textComponent == null) textComponent = GetComponent<TMP_Text>();
    }
}
