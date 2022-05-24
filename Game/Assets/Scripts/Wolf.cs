using System;
using UnityEngine;

public class Wolf : Monster
{
    public LayerMask ground;

    public float agrDistance = 4.0f;
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    private float currentdashDuration;
    private bool isDashing = false;
    private bool canNotDash = false;

    private Rigidbody2D rb;
    private Animator anim;

    private enum MState
    {
        Idle,
        Run,
    }

    private MState State
    {
        get { return (MState)anim.GetInteger("Stare"); }
        set { anim.SetInteger("Stare", (int)value); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        Fall();
    }

    private void Update()
    {
        if (isDashing)
        {
            Dash();
            State = MState.Run;
        }
        else if (player == null) rb.Sleep();
        else if (player != null)
        {
            Flip();
            State = MState.Idle;
            var distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= agrDistance && !canNotDash)
            {
                isDashing = true;
                canNotDash = true;
                Invoke(nameof(DashLock), dashCooldown);
                currentdashDuration = dashDuration;
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void Dash()
    {
        if (currentdashDuration <= 0)
        {
            isDashing = false;
            rb.velocity = Vector2.zero;
        }
        else
        {
            currentdashDuration -= Time.deltaTime;
            if (transform.localScale.x > 0) rb.velocity = new Vector2(-dashSpeed, rb.velocity.y); //Vector2.left * dashSpeed;
            else rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
        }
    }

    private void DashLock()
    {
        canNotDash = false;
    }

    //private void Flip()
    //{
    //    var distance = transform.position.x - player.transform.position.x;
    //    transform.localScale = new Vector2(Math.Sign(distance), 1);
    //}

    private void Fall()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.16f, ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 3;
        }
    }

    public override void WhenReceiveDamage()
    {
        Debug.Log(Lifes);
        currentdashDuration = 0;
    }

    public override void Die()
    {
        anim.SetTrigger("Die");
        rb.velocity = Vector2.zero;
        gameObject.layer = 0;
        Destroy(this);
    }
}
