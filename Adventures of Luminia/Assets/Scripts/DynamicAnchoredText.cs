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
        StartCoroutine(ShowTextTimer(seconds));
    }

    private void Update()
    {
        var textPosition = Camera.main.WorldToScreenPoint(anchor.position + offset);
        text.transform.position = textPosition;
    }

    private IEnumerator ShowTextTimer(float seconds)
    {
        text.color = new Color(1f, 1f, 1f, 1f);

        yield return new WaitForSeconds(seconds);

        text.color = new Color(1f, 1f, 1f, 0f);
    }
}
