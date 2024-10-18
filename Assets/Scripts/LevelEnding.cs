using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnding : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    private void Start()
    {
        fadeCanvasGroup.alpha = 0f;
    }

    public void Restart()
    {
        StartCoroutine(FadeOutAndReset());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            StartCoroutine(LevelCompleted());
        }
    }

    IEnumerator LevelCompleted()
    {
        // Confetti
        GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(3f);

        StartCoroutine(FadeOutAndReset());
    }

    IEnumerator FadeOutAndReset()
    {
        // Fade out
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.unscaledDeltaTime; // using 'unscaledDeltaTime' to work while timeScale = 0
            yield return null;
        }
        fadeCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(1f);

        // Regenerate maze
        FindObjectOfType<MazeGenerator>().GenerateMaze();

        // Respawn player
        GetComponent<PlayerController>().Respawn();

        // Fade in
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f;
    }
}
