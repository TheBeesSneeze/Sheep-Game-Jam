using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("MainScene");
        PlayerController.Instance.UnPause();
    }

    public void QuitGame()
    {
        print("Exited");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        PlayerController.Instance.UnPause();
    }

    public void LoadWakeUpScene()
    {
        SceneManager.LoadScene("WakeUp");

        if (PlayerController.Instance != null)
            PlayerController.Instance.UnPause();
    }

    public void LoadWakeUpScene2()
    {
        SceneManager.LoadScene("WakeUp2");
    }
}
