using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaticAnchoredText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Transform anchor;
    [SerializeField] private Vector3 offset;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        var textPosition = mainCamera.WorldToScreenPoint(anchor.position + offset);
        text.transform.position = textPosition;
    }

    public void ShowText()
    {
        text.gameObject.SetActive(true);
    }

    public void HideText()
    {
        text.gameObject.SetActive(false);
    }
}

