using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private string nextSceneName;

    private bool isFading = false;

    private IEnumerator StartTransition()
    {
        isFading = true;
        fadeImage.gameObject.SetActive(true);

        // Gradual Darkening
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            fadeImage.color = Color.Lerp(Color.clear, Color.black, normalizedTime);
            yield return null;
        }
        fadeImage.color = Color.black;

        // Load the Next Scene
        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator FadeIn()
    {
        fadeImage.color = Color.black;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            fadeImage.color = Color.Lerp(Color.black, Color.clear, normalizedTime);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
        isFading = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isFading)
        {
            StartCoroutine(StartTransition());
        }
    }
}
