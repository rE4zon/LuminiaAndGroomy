using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DynamicAnchoredText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Transform anchor;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float seconds;

    private void Start()
    {
        text.color = new Color(1f, 1f, 1f, 0f);
    }

    public void ShowText()
    {
        StartCoroutine(ShowTextCoroutine(seconds));
    }

    private void Update()
    {
        var textPosition = Camera.main.WorldToScreenPoint(anchor.position + offset);
        text.transform.position = textPosition;
    }

    private IEnumerator ShowTextCoroutine(float seconds)
    {
        // Плавное появление текста
        for (float t = 0; t < 1; t += Time.deltaTime / seconds)
        {
            text.color = new Color(1f, 1f, 1f, t);
            yield return null;
        }
        text.color = new Color(1f, 1f, 1f, 1f);

        yield return new WaitForSeconds(seconds);

        // Плавное исчезновение текста
        for (float t = 1; t > 0; t -= Time.deltaTime / seconds)
        {
            text.color = new Color(1f, 1f, 1f, t);
            yield return null;
        }
        text.color = new Color(1f, 1f, 1f, 0f);
    }
}
