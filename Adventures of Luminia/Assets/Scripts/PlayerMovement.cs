using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private LayerMask notPlayerMask;

    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider collider;
    private bool isGrounded;

    private void Start()
    {
        collider = GetComponent<CapsuleCollider>();
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
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            UnCrouch();
        }

        if (Physics.CheckSphere(groundChecker.position, 0.3f, notPlayerMask))
        {
            animator.SetBool("isInAir", false);
            isGrounded = true;
        }
        else
        {
            animator.SetBool("isInAir", true);
            isGrounded = false;
        }
    }
    void Jump()
    {
        if (animator.GetBool("isCrouching"))
        {
            return;
        }
        if (isGrounded)
        {
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("Did not find ground layer");
        }
    }

    void Crouch()
    {
        if (isGrounded)
        {
            animator.SetBool("isCrouching", true);
            speed = 1f;
            collider.height = 0.7f;
            collider.center = new Vector3(collider.center.x, 0.30f, collider.center.z);
        }
    }

    private void UnCrouch()
    {
        animator.SetBool("isCrouching", false);
        speed = 2f;
        collider.height = 1.25f;
        collider.center = new Vector3(collider.center.x, 0.62f, collider.center.z);

    }
}