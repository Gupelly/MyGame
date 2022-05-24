using System;
using UnityEngine;

public class FlyingEye : Monster
{
    public LayerMask ground;
    public float agrDistance = 4.0f;
    private bool chase = false;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (IsDead && Physics2D.OverlapCircle(transform.position, 0.1f, ground))
        {
            rb.velocity = Vector3.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            Destroy(this);
        }
    }

    private void Update()
    {
        if (player == null) rb.Sleep();
        else if (player != null)
        {
            var distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= agrDistance) chase = true;
            if (chase) Chase();
            Flip(-1);
        }
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    //private void Flip()
    //{
    //    var distance = transform.position.x - player.transform.position.x;
    //    transform.localScale = new Vector2(-Math.Sign(distance), 1);
    //}

    public override void Die()
    {
        anim.SetTrigger("Death");
        gameObject.layer = 0;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Dynamic;
        bc.enabled = false;
        IsDead = true;
    }
}
