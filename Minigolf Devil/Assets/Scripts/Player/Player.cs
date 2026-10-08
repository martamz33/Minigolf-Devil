using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Movement Stats")]
    public float moveSpeed = 15f;
    public float jumpForce = 7f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    // Variables
    private Rigidbody2D rg;
    private Collider2D col;
    private float horizontalInput;
    private bool jumpRequest;
    private bool isGrounded;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        // Important for precision games
        rg.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    // Update for Input
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true;
        }
    }

    // Fixed Update Physics
    void FixedUpdate()
    {
        CheckGrounded();
        MoveBall();
        HandleJump();
    }

    // Functions of Physics
    private void MoveBall()
    {
        rg.AddForce(new Vector2(horizontalInput * moveSpeed, 0f), ForceMode2D.Force);
    }

    private void HandleJump()
    {
        if(jumpRequest)
        {
            rg.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequest = false;
        }
    }

        private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
