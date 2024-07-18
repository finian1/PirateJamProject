using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float horizontal;
    [SerializeField] private float currentSpeed = 8f;
    [SerializeField] private float originalSpeed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private bool isAirborne = false;
    [SerializeField] private bool hasJumped = false;

    [SerializeField] private int jumpCount;
    [SerializeField] private int maxJumpCount;

    [SerializeField] private bool isCrouching = false;
    [SerializeField] private Vector3 crouchScale;
    [SerializeField] private Vector3 originalScale;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private bool isGrounded;

    private void Start()
    {
        originalScale = transform.localScale;
        crouchScale = new Vector3(originalScale.x, originalScale.y * 0.5f, originalScale.z);
    }

    private void Update()
    {

        CheckGrounded();

        Movement();

        Flip();
        Crouch();

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);
    }

    void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && jumpCount != maxJumpCount)
        {
            hasJumped = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (hasJumped && isAirborne)
        {
            jumpCount++;
            hasJumped = false;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 1f);
        }

        if (isGrounded)
        {
            isAirborne = false;
            jumpCount = 0;
        }

        else
        {
            isAirborne = true;
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void Crouch()
    {
        if(Input.GetButton("Crouch"))
        {
            if(!isCrouching)
            {
                isCrouching = true;
                float heightDifference = transform.localScale.y - crouchScale.y;
                transform.position = new Vector3(transform.position.x, transform.position.y - heightDifference * 0.8f, transform.position.z);

                if(isFacingRight)
                {
                    transform.localScale = crouchScale;
                }

                else if(!isFacingRight)
                {
                    Vector3 currentScale = crouchScale;
                    currentScale.x *= -1f;
                    transform.localScale = currentScale;
                }

                currentSpeed = originalSpeed * 0.5f;

            }
        }

        else if(isCrouching)
        {
            isCrouching = false;
            float heightDifference = transform.localScale.y - crouchScale.y;
            transform.position = new Vector3(transform.position.x, transform.position.y + heightDifference * 0.8f, transform.position.z);

            if(isFacingRight)
            {
                transform.localScale = originalScale;
            }

            else if (!isFacingRight)
            {
                Vector3 currentScale = originalScale;
                currentScale.x *= -1f;
                transform.localScale = currentScale;
            }

            currentSpeed = originalSpeed;

        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        if(groundCheck)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }

}
