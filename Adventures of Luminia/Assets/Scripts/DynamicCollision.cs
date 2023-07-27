using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCollision : MonoBehaviour
{
    [SerializeField] private DynamicAnchoredText DynamicAnchoredText;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            DynamicAnchoredText.ShowText();
        }
    }
}
