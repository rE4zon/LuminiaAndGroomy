using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed = 2f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] public Transform groundChecker;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] public LayerMask notPlayerMask;
    [SerializeField] private AudioSource audioSource; // Привяжите ваш объект AudioSource в инспекторе

    public Animator animator;
    private Rigidbody rb;
    public CapsuleCollider _collider;
    private float smoothVelocity;
    private bool isGrounded;
    private bool isMoving = false;
    private bool isCrouching = false;

    private void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            Crouch();
        }
        if (Input.GetKeyUp(KeyCode.C))
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

        if (rb.velocity.magnitude > 0.1f && isGrounded)
        {
            PlayFootstepSound();
            isMoving = true;
            float targetPitch = animator.GetBool("isCrouching") ? 0.5f : 1.0f;
            float currentPitch = audioSource.pitch;
            float smoothTime = 0.15f; // Время плавного изменения высоты звука

            audioSource.pitch = Mathf.SmoothDamp(currentPitch, targetPitch, ref smoothVelocity, smoothTime);
        }
        else
        {
            isMoving = false;
            audioSource.pitch = 1.0f;
            if (audioSource.pitch > 0.99f)
            {
                audioSource.Stop();
            }
        }
    }

    private void PlayFootstepSound()
    {
        if (!audioSource.isPlaying && isMoving)
        {
            if (animator.GetBool("isCrouching"))
            {
                float targetPitch = Mathf.Lerp(0.5f, 0.75f, rb.velocity.magnitude / 0.75f);
                audioSource.pitch = targetPitch; // If the player is crouching, the sound is slowed down based on speed
            }
            else
            {
                float targetPitch = Mathf.Lerp(1.0f, 0.75f, rb.velocity.magnitude / 0.75f);
                audioSource.pitch = targetPitch; // If the player is not crouching, the sound is slowed down based on speed
            }
            audioSource.Play();
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
            _collider.height = 0.5f;
            _collider.center = new Vector3(_collider.center.x, 0.30f, _collider.center.z);

            isCrouching = true;
        }
    }

    private void UnCrouch()
    {
        animator.SetBool("isCrouching", false);
        speed = 2f;
        _collider.height = 1.25f;
        _collider.center = new Vector3(_collider.center.x, 0.62f, _collider.center.z);

        isCrouching = false;
    }
}
