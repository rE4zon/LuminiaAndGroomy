using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1.0f;

    private bool isFading = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isFading)
        {
            StartCoroutine(PlayerRespawn());
        }
    }

    private IEnumerator PlayerRespawn()
    {
        isFading = true;

        // Fade Out (Darken the screen)
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.clear;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            fadeImage.color = Color.Lerp(Color.clear, Color.black, normalizedTime);
            yield return null;
        }
        fadeImage.color = Color.black;

        // Load the current scene again
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Fade In (Lighten the screen after respawn)
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            fadeImage.color = Color.Lerp(Color.black, Color.clear, normalizedTime);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
        isFading = false;
    }
}
