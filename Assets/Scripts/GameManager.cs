using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        PlayerController.Instance.UnPause();
    }

    public void LoadWakeUpScene2()
    {
        SceneManager.LoadScene("WakeUpScene2");
    }
}
