using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            UIManager.instance.UpdateTitle("Box Stage");
            UIManager.instance.PauseGameMenu(true);
        }
        else
        {
            Debug.Log("SCENE 1");
            UIManager.instance.UpdateTitle("Maze Stage");
            UIManager.instance.PauseGameMenu(true);
        }
    }

    public void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EndGame()
    {
        SceneManager.LoadScene(2);
        UIManager.instance.EndGameMenu();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        UIManager.instance.PauseGameMenu(false);
        PauseGame();
        isPaused = true;
    }
}