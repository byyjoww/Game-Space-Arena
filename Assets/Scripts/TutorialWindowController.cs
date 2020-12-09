using Elysium.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWindowController : MonoBehaviour
{
    public GameObject tutorialPanel;

    private PauseMenu pause;

    private void Awake()
    {
        pause = GetComponent<PauseMenu>();

        if (SessionSettings.showTutorial)
        {
            SetTutorialTimer(0.5f);
        }
    }

    public void SetTutorialTimer(float time)
    {
        var timer = Timer.CreateTimer(time);
        timer.OnTimerEnd += ShowPanel;
    }

    public void ShowPanel()
    {
        tutorialPanel.SetActive(true);
        pause.StopTime();
    }

    public void HidePanel()
    {
        tutorialPanel.SetActive(false);
        pause.StartTime();
        SessionSettings.showTutorial = false;
    }
}

public static class SessionSettings
{
    public static bool showTutorial = true;
}