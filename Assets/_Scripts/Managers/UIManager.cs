using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI actionName;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI title;
    public TextMeshProUGUI menuTitle;
    public Color titleColor;
    public Color titleAlpha;
    public GameObject pauseMenu;
    public GameObject resumeButton;

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

    public void UpdateCoins(int value)
    {
        coins.text = value.ToString();
    }

    public void UpdateAction(string action)
    {
        actionName.text = action;
    }

    public void UpdateTitle(string value)
    {
        title.text = value;
        StartCoroutine(DisplayTitle());
    }

    IEnumerator DisplayTitle()
    {
        float percent = 0;
        WaitForFixedUpdate update = new WaitForFixedUpdate();

        while (percent < 1f)
        {
            percent += Time.deltaTime;
            title.color = Color.Lerp(titleAlpha, titleColor, percent / 1f);
            yield return update;
        }

        title.color = titleColor;

        yield return new WaitForSeconds(1.5f);

        percent = 0;

        while (percent < 1f)
        {
            percent += Time.deltaTime;
            title.color = Color.Lerp(titleColor, titleAlpha, percent / 1f);
            yield return update;
        }

        title.color = titleAlpha;
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (GameManager.instance.isPaused)
        {
            GameManager.instance.ResumeGame();
        }
        else
        {
            GameManager.instance.PauseGame();
        }

        GameManager.instance.isPaused = !GameManager.instance.isPaused;
    }

    public void PauseGameMenu(bool active)
    {
        //actionName.gameObject.transform.parent.gameObject.SetActive(active);
        menuTitle.text = "Game Paused";
        resumeButton.SetActive(true);
    }

    public void EndGameMenu()
    {
        pauseMenu.SetActive(true);
        menuTitle.text = "Game Finished";
        resumeButton.SetActive(false);
    }
}