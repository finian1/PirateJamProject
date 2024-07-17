using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private bool isAirborne = false;
    [SerializeField] private bool hasJumped = false;


    [SerializeField] private int jumpCount;
    [SerializeField] private int maxJumpCount;

    [SerializeField] private float fallingVelocity;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private bool isGrounded;


    private void Update()
    {

        CheckGrounded();

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && jumpCount != maxJumpCount)
        {
            hasJumped = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if(hasJumped && isAirborne)
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

        Flip();

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
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

    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
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
