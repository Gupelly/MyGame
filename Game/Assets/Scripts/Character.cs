using System.Collections;
using UnityEngine;
using System;

public class Character : Alive
{
    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private float jumpForce = 13.0f;
    [SerializeField]
    private float doubleJumpForce = 10.0f;
    [SerializeField]
    private float dashSpeed = 50f;
    [SerializeField]
    private float dashDuration = 0.1f;
    [SerializeField]
    private float dashCooldown = 1.5f;
    [SerializeField]
    private float attackRange = 0.5f;
    [SerializeField]
    private float attackCooldown = 0.5f;

    private bool unlockDash = false;
    private bool unlockDoubleJump = false;

    public LayerMask ground;

    public Transform WallCheck;
    private bool IsWall = false;

    public Transform GroundCheck;
    private bool isGrounded = false;
    private bool isDoubleJump = false;

    private bool isDashing = false;
    private float currentdashDuration;
    private bool canNotDash = false;
    private bool jumpAfterDash = false;

    public LayerMask monster;
    public Transform AttackPos;
    private bool canNotAttack = false;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool faceRight = true;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Lifes = 3;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    void Update()
    {
        if (isDashing)
        {
            Dash();
            if (Input.GetButton("Jump")) jumpAfterDash = true;
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && !canNotDash)
            {
                isDashing = true;
                canNotDash = true;
                Invoke("DashLock", dashCooldown);
                currentdashDuration = dashDuration;
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0;
            }

            if (Input.GetButton("Horizontal")) Run();

            if (Input.GetButtonDown("Jump") || jumpAfterDash)
            {
                jumpAfterDash = false;
                if (isGrounded) Jump(jumpForce);
                else if (isDoubleJump)
                {
                    Jump(doubleJumpForce);
                    isDoubleJump = false;
                }
            }

            if (Input.GetButton("Fire2") && !canNotAttack) Attack();
        }
    }

    private void CheckGround()
    {
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1F);
        IsWall = Physics2D.OverlapCircle(WallCheck.position, 0.05F, ground);
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.3F, ground);
        if (isGrounded) isDoubleJump = true;
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        if (IsWall && transform.localScale.x * direction.x > 0) direction.x = 0;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        //transform.localScale *= new Vector2(Math.Sign(direction.x), 1);
        //sprite.flipX = direction.x < 0.0f;

        if ((direction.x < 0 && faceRight) || (direction.x > 0 && !faceRight))
        {
            faceRight = !faceRight;
            transform.localScale *= new Vector2(-1, 1);
        }
    }

    private void Jump(float jumpForce)
    {
        //rb.velocity = Vector2.zero;
        //rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Dash()
    {
        if (currentdashDuration <= 0)
        {
            isDashing = false;
            rb.gravityScale = 2.8f;
            rb.velocity = Vector2.zero;
        }
        else
        {
            currentdashDuration -= Time.deltaTime;
            if (transform.localScale.x > 0) rb.velocity = Vector2.right * dashSpeed;
            else rb.velocity = Vector2.left * dashSpeed;
        }
    }

    private void DashLock()
    {
        canNotDash = false;
    }

    private void Attack()
    {
        canNotAttack = true;
        Invoke("AttackLock", attackCooldown);
        var enemies = Physics2D.OverlapCircleAll(AttackPos.position, attackRange, monster);
        foreach (var enemy in enemies)
            enemy.GetComponent<Monster>().ReceiveDamage();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPos.position, attackRange);
    }

    private void AttackLock()
    {
        canNotAttack = false;
    }
}