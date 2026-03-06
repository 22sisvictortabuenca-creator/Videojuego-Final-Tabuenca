using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f; 
    public float jumpForce = 7f;
    
    private Rigidbody2D rb;
    private Animator anim;
    public bool isGrounded;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float currentSpeed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
        
        rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);

        
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        if (moveInput > 0 && isFacingRight == false)
        {
            FlipPlayer();
        }
        else if (moveInput < 0 && isFacingRight == true)
        {
            FlipPlayer();
        }
        
        if (anim != null)
        {
            anim.SetFloat("Speed", Mathf.Abs(moveInput * currentSpeed));
            anim.SetBool("IsGrounded", isGrounded);
        }
    }

    void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
}