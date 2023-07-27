using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingSystem : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] private float ChangedRadius = 0.3622935f;
    [SerializeField] private float ChangedCenterZ = 0.2f;
    bool isPushing;
    bool isInsideCollider;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInsideCollider = true;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isInsideCollider = false;
        StopPushing();
    }

    private void Start()
    {
        isPushing = false;
    }

    private void Update()
    {
        if (isInsideCollider)
        {
            Vector3 playerToPushableObject = transform.position - playerMovement.transform.position;
            Vector3 playerLookDirection = playerMovement.transform.forward;
            playerToPushableObject.y = 0f; // Нам нужны только горизонтальные компоненты векторов

            float angle = Vector3.Angle(playerLookDirection, playerToPushableObject);

            // Проверяем, не стоит ли игрок на поверхности объекта (значение y близко к 0)
            bool isPlayerOnSurface = Mathf.Abs(playerToPushableObject.y) < 0.1f;

            

            if (isPlayerOnSurface && angle < 45f)
            {
                if (!isPushing)
                {
                    StartPushing();
                }
            }
            else
            {
                if (isPushing)
                {
                    StopPushing();
                }
            }
        }
        else
        {
            if (isPushing)
            {
                StopPushing();
            }
        }
    }

    private void StartPushing()
    {
        isPushing = true;
        playerMovement.animator.SetBool("isPushing", true);
        playerMovement.speed = 0.75f;
        playerMovement._collider.center = new Vector3(playerMovement._collider.center.x, playerMovement._collider.center.y, ChangedCenterZ);
        playerMovement._collider.radius = ChangedRadius;
    }

    private void StopPushing()
    {
        isPushing = false;
        playerMovement.animator.SetBool("isPushing", false);
        playerMovement.speed = 2f;
        playerMovement._collider.center = new Vector3(-0.004384995f, playerMovement._collider.center.y, 0f);
        playerMovement._collider.radius = 0.06834984f;
    }
}
