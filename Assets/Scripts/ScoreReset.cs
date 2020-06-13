using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreReset : MonoBehaviour
{
    private void Start()
    {
        Database d = Database.i;
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();
    }
}
