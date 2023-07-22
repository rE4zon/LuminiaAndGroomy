using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float camPositionSpeed = 10f;
    [SerializeField] private float verticalFollowSpeed = 5f;
    [SerializeField] private float maxHeightDistance = 5f;
    [SerializeField] private float minHeightDistance = 2f; 

    private bool followPlayerVertically = false;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        
        Vector3 desiredPosition = playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, camPositionSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        
        if (followPlayerVertically)
        {
            float targetVerticalPosition = Mathf.Clamp(playerTransform.position.y, playerTransform.position.y - minHeightDistance, playerTransform.position.y + maxHeightDistance);

            
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit groundHit, maxHeightDistance))
            {
                float groundHeight = groundHit.point.y;
                if (groundHeight > targetVerticalPosition)
                {
                    targetVerticalPosition = groundHeight;
                }
            }

            targetPosition = new Vector3(transform.position.x, targetVerticalPosition, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, verticalFollowSpeed * Time.deltaTime);
        }

        
        transform.LookAt(playerTransform);
    }

    public void ToggleFollowPlayerVertically(bool shouldFollow)
    {
        followPlayerVertically = shouldFollow;
    }
}
