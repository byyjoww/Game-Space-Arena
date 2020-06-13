using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public void LoadNextLevel ()    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetLevel ()    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}