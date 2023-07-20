using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float camPositionSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newCamPos = new Vector3(playerTransform.position.x + offset.x, transform.position.y, playerTransform.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, newCamPos, camPositionSpeed * Time.deltaTime);
    }
}
