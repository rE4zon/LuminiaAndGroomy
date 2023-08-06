using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LampSystem : MonoBehaviour
{
    [SerializeField] private Light LampLight;
    [SerializeField] private AudioSource audioSource;

    private static bool isOn = false;

    private void Start()
    {
        LampLight.enabled = isOn;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                audioSource.Play();
                isOn = !isOn;
                LampLight.enabled = isOn;
            }
        }
    }
    
    



}