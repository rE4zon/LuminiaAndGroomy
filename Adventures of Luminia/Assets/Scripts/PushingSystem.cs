using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingSystem : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] private float ChangedRadius = 0.3622935f;
    [SerializeField] private float ChangedCenterZ = 0.2f;
    bool isPushing;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartPushing();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        StopPushing();
    }

    private void StartPushing()
    {

        isPushing = true;
        playerMovement.animator.SetBool("isPushing", true);
        playerMovement.speed = 0.5f;
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

    private void Update()
    {
        if (isPushing)
        {
            // Проверяем направление взгляда игрока
            Vector3 playerToPushableObject = transform.position - playerMovement.transform.position;
            Vector3 playerLookDirection = playerMovement.transform.forward;
            playerToPushableObject.y = 0f; // Нам нужны только горизонтальные компоненты векторов

            // Угол между направлением взгляда и направлением на объект (в градусах)
            float angle = Vector3.Angle(playerLookDirection, playerToPushableObject);

            // Если игрок смотрит на объект в угле менее 45 градусов, то он может продолжать его толкать
            if (angle < 45f)
            {
                StartPushing();
            }
            else
            {
                // Если игрок больше не смотрит на объект, прекратить его толкать
                StopPushing();
            }
        }
    }
}