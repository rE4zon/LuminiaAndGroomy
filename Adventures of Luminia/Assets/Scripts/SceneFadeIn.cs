using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1.0f;

    private IEnumerator Start()
    {
        fadeImage.gameObject.SetActive(true);

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            fadeImage.color = Color.Lerp(Color.black, Color.clear, normalizedTime);
            yield return null;
        }

        fadeImage.color = Color.clear;
        fadeImage.gameObject.SetActive(false);
    }
}
