using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    [SerializeField] Transform target; 
    [SerializeField] float rotationSpeed = 5f; 

    private Vector3 offset; // Расстояние от камеры до целевого объекта
    private float rotationAngle = 0f; // Текущий угол вращения камеры

    private void Start()
    {
        if (target == null)
        {
            
            enabled = false;
            return;
        }

        
        offset = transform.position - target.position;
    }

    private void Update()
    {
        
        rotationAngle += rotationSpeed * Time.deltaTime;

       
        Vector3 newPosition = Quaternion.Euler(0f, rotationAngle, 0f) * offset;

       
        transform.position = target.position + newPosition;

        
        transform.LookAt(target);
    }
}
