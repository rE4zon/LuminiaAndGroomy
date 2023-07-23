using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LampSystem : MonoBehaviour
{
    [SerializeField] private Light LampLight;
    [SerializeField] private TMP_Text Text;
    [SerializeField] private bool isOn;
    

    private void Start()
    {
        LampLight.enabled = isOn;
        
        Text.gameObject.SetActive(false); // Initially hide the text
    }

    

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Text.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                isOn = !isOn;
                LampLight.enabled = isOn;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Text.gameObject.SetActive(false);
        }
    }
}


