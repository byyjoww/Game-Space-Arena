using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject objectToToggle;
    public bool isPaused;

    public void Toggle()
    {
        if (objectToToggle.activeInHierarchy == false)
        {
            if (isPaused == false) StopTime();
            objectToToggle.SetActive(true);
        }
        else if (objectToToggle.activeInHierarchy == true)
        {            
            objectToToggle.SetActive(false);
            if (isPaused == true) StartTime();
        }
    }

    public void MainMenu()
    {
        if (isPaused == true) StartTime();
        GetComponent<LoadMainMenu>().LoadMenu();
    }

    public void Restart()
    {
        if (isPaused == true) StartTime();
        GetComponent<LevelLoader>().ResetLevel();
    }
    
    public void StopTime()
    {
        if (isPaused == false)
        {
            Time.timeScale = 0f;
            isPaused = true;
        }
        else Debug.Log("Time is already stopped!");
    }

    public void StartTime()
    {
        if (isPaused == true)
        {
            Time.timeScale = 1f;
            isPaused = false;
        }
        else Debug.Log("Time is already ticking!");
    }
}
