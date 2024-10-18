using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject pauseUI;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    private bool isPaused = false;

    void Start()
    {
        pauseUI.SetActive(false);
        fadeCanvasGroup.alpha = 0f;
    }

    // Method is triggered on button click
    public void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        Debug.Log("Game resumed");
        StartCoroutine(UnfadeFromBlack());
        ToggleScreenUIs();
        Time.timeScale = 1f; // Starting time
        isPaused = false;
    }

    void Pause()
    {
        Debug.Log("Game paused");
        StartCoroutine(FadeToBlack());
        ToggleScreenUIs();
        Time.timeScale = 0f; // Stoping time
        isPaused = true;
    }

    // Animation of screen fading
    IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.unscaledDeltaTime; // using 'unscaledDeltaTime' to work while timeScale = 0
            yield return null;
        }
        fadeCanvasGroup.alpha = 1f;
    }

    // Animation of screen unfading
    IEnumerator UnfadeFromBlack()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f;
    }

    // Method for switching 'Pause' and 'Game' UIs
    private void ToggleScreenUIs()
    {
        gameUI.SetActive(!gameUI.activeSelf);
        pauseUI.SetActive(!pauseUI.activeSelf);
    }

    public void QuitGame()
    {
        Application.Quit();

        // For testing Unity editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
