using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LampSystem : MonoBehaviour
{
    [SerializeField] private Light LampLight;
    
    private static bool isOn = false;

    private void Start()
    {
        LampLight.enabled = isOn;
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOn = !isOn;
                LampLight.enabled = isOn;
            }
        }
    }

   

   
}