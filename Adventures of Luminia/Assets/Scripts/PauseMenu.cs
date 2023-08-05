using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    static bool GameIsPaused = false;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] Image fadePanel;
    public float fadeDuration = 0.5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        StartCoroutine(FadeOutAndLoadMenu());
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    IEnumerator FadeOutAndLoadMenu()
    {
        float elapsedTime = 0f;
        Color initialColor = Color.clear;
        Color targetColor = Color.black;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(initialColor.a, targetColor.a, elapsedTime / fadeDuration);
            fadePanel.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
            yield return null;
        }

        fadePanel.color = targetColor;

        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
