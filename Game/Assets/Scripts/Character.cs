using System.Collections;
using UnityEngine;
using System;

public class Character : Alive
{
    [SerializeField]
    private float speed = 4.0f;
    [SerializeField]
    private float jumpForce = 14.0f;
    [SerializeField]
    private float doubleJumpForce = 10.0f;
    [SerializeField]
    private float dashSpeed = 8f;
    [SerializeField]
    private float dashDuration = 0.5f;
    [SerializeField]
    private float dashCooldown = 1.5f;
    [SerializeField]
    private float attackRange = 0.9f;
    [SerializeField]
    private float attackCooldown = 0.5f;

    public bool unlockDash = false;
    public bool unlockDoubleJump = false;

    public LayerMask ground;

    public Transform Centre;
    private bool IsWall = false;
    private bool isGrounded = false;
    private bool isDoubleJump = false;

    private bool isDashing = false;
    public bool isJumpBack;
    public float jumpBackDirection = 1;
    private float currentdashDuration;
    private bool canNotDash = false;
    private bool jumpAfterDash = false;

    public LayerMask monster;
    private bool canNotAttack = false;

    public LayerMask Bullet;

    public Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool faceRight = true;
    private Animator anim;

    public enum CharState
    {
        Idle,
        Run, 
        Jump,
        Fall,
        Attack,
        Dash,
        JumpBack
    }

    private CharState State
    {
        get { return (CharState) anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Centre = GetComponentsInChildren<Transform>()[2];

        ground = LayerMask.GetMask("Ground");
        monster = LayerMask.GetMask("Monster");
        Bullet = LayerMask.GetMask("Bullet");
        Lifes = 3;
    }

    private void FixedUpdate()
    {
        if (IsInvisable && !isDashing) Blink();
        else sprite.gameObject.SetActive(true);
        CheckGround();
    }

    void Update()
    {
        if (isJumpBack)
        {
            JumpBack();
            State = CharState.JumpBack;
        }
        else if (isDashing)
        {
            Dash();
            State = CharState.Dash;
            if (Input.GetButton("Jump")) jumpAfterDash = true;
        }
        else
        {
            if (isGrounded && !Input.GetButton("Jump") && !canNotAttack) State = CharState.Idle;
            if (Input.GetButtonDown("Fire1") && !canNotDash)
            {
                isDashing = true;
                canNotDash = true;
                IsInvisable = true;
                Invoke(nameof(DashLock), dashCooldown);
                currentdashDuration = dashDuration;
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0;
            }

            if (Input.GetButton("Horizontal")) Run();

            if (Input.GetButtonDown("Jump") || jumpAfterDash)
            {
                jumpAfterDash = false;
                if (isGrounded) Jump(jumpForce);
                else if (isDoubleJump && unlockDoubleJump)
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
        IsWall = Physics2D.OverlapCircle(Centre.position, 0.05F, ground);
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.4F, ground);
        if (isGrounded) isDoubleJump = true;
        if (!isGrounded && State != CharState.Jump && !canNotAttack) State = CharState.Fall;
    }

    private void Run()
    {
        var direction = transform.right * Input.GetAxis("Horizontal");
        if (IsWall && transform.localScale.x * direction.x > 0) direction.x = 0;
        transform.position = Vector2.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        if (direction.x != 0 && isGrounded && !Input.GetButton("Jump") && !canNotAttack) State = CharState.Run;
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
        if (State != CharState.Attack) State = CharState.Jump;
    }

    private void Dash()
    {
        if (currentdashDuration <= 0)
        {
            isDashing = false;
            IsInvisable = false;
            State = CharState.Idle;
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
        State = CharState.Attack;
        canNotAttack = true;
        Invoke("AttackLock", attackCooldown);       
        var enemies = Physics2D.OverlapCircleAll(Centre.position, attackRange, monster);
        foreach (var enemy in enemies)
            enemy.GetComponent<Monster>().ReceiveDamage();
        //var enemy = Physics2D.OverlapCircle(AttackPos.position, attackRange, monster);
        //enemy.GetComponent<Monster>().ReceiveDamage();
        //var bullet = Physics2D.OverlapCircle(AttackPos.position, attackRange, Bullet);
        //bullet.GetComponent<Bullet>().ReceiveDamage();
        var bullets = Physics2D.OverlapCircleAll(Centre.position, attackRange, Bullet);
        foreach (var bullet in bullets)
            bullet.GetComponent<Bullet>().ReceiveDamage();
        //transform.position = new Vector2(rb.position.x -0.2f * transform.localScale.x, rb.position.y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Centre.position, attackRange);
    }

    private void AttackLock()
    {
        canNotAttack = false;
    }

    public override void WhenReceiveDamage()
    {
        if (isDashing) return;
        rb.velocity = Vector2.zero;
        IsInvisable = true;
        Invoke("DisableInvisable", 2f);
        rb.velocity = new Vector2(rb.velocity.x, 6f);
        transform.position = new Vector2(rb.position.x - 1.5f * transform.localScale.x, rb.position.y);
    }

    private void DisableInvisable()
    {
        IsInvisable = false;
    }

    private void Blink()
    {
        sprite.gameObject.SetActive(!sprite.gameObject.activeSelf);
    }

    public override void Die()
    {
        anim.SetTrigger("Death");
        anim.SetInteger("State", -1);
        gameObject.layer = 0;
        Destroy(this);
    }

    public void JumpBack()
    {
        rb.velocity = new Vector2(jumpBackDirection * 6, rb.velocity.y);
    }
}