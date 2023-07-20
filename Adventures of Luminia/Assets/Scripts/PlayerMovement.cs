using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float jumpForce = 2f;

    [SerializeField] private LayerMask groundLayer;

    private Animator animator;
    private Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 directionVector = new Vector3(horizontal, 0, vertical);
        animator.SetFloat("Speed", Vector3.ClampMagnitude(directionVector, 0.5f).magnitude);
        if (directionVector.magnitude > Mathf.Abs(0.05f))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionVector), Time.deltaTime * rotationSpeed);
        }
        Vector3 moveDir = Vector3.ClampMagnitude(directionVector, 1) * speed;
        rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);
        rb.angularVelocity = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }

    }
    void Jump()
    {
        
        if (Physics.Raycast(groundChecker.position, Vector3.down, 0.2f, groundLayer))
        {
            
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("Did not find ground layer");
        }
    }

    void Crouch()
    {
        if (Physics.Raycast(groundChecker.position, Vector3.down, 0.2f, groundLayer))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}