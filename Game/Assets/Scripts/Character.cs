using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private int lifes = 3;
    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private float jumpForce = 13.0f;
    [SerializeField]
    private float doubleJumpForce = 10.0f;

    private bool unlockDoubleJump = false;

    public LayerMask ground;

    public Transform rightWallCheck;
    private bool IsRightWall = false;

    public Transform leftWallCheck;
    private bool IsLeftWall = false;

    private bool isGrounded = false;
    private bool isDoubleJump = false;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    void Update()
    {
        if (Input.GetButton("Horizontal")) Run();
        if (Input.GetButtonDown("Jump"))
        {        
            if (isGrounded) Jump(jumpForce);
            else if (isDoubleJump)
            {
                Jump(doubleJumpForce);
                isDoubleJump = false;
            }
        }
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        if (direction.x > 0 && IsRightWall || direction.x < 0 && IsLeftWall) direction.x = 0;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0.0f;
    }

    private void Jump(float jumpForce)
    {
        //rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void CheckGround()
    {
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1F);
        IsRightWall = Physics2D.OverlapCircle(rightWallCheck.position, 0.01F, ground);
        IsLeftWall = Physics2D.OverlapCircle(leftWallCheck.position, 0.01F, ground);
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.3F, ground);
        if (isGrounded) isDoubleJump = true;
    }
}