using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    [SerializeField] private Light playerLight;
    [SerializeField] private float lightDistance = 10f;
    [SerializeField] private float lightIntensity = 1.5f;

    private void Update()
    {
        
        playerLight.transform.position = transform.position;

       
        playerLight.range = lightDistance;
        playerLight.intensity = lightIntensity;
    }
}
