using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveMonster : Monster
{
    public float agrDistance = 4.0f;

    public LayerMask ground;

    public Transform WallCheck;
    private bool IsWall;

    public LayerMask Player;
    private Transform playerPos;
    public BoxCollider2D bc;
    private bool chase = false;
    public bool isReborn;

    private Rigidbody2D rb;
    private Animator anim;
    

    private enum MState
    {
        Idle,
        Run,
        Reborn,
        Fall
    }

    private MState State
    {
        get { return (MState)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    void Awake()
    {
        //gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Invoke("RebornFalse", 0.5f);
        bc = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //if (speed == 0) anim.SetTrigger("auch");
        if (Physics2D.OverlapCircle(transform.position, agrDistance, Player))
        {
            var newPlayer = Physics2D.OverlapCircle(transform.position, agrDistance, Player);
            playerPos = newPlayer.GetComponent<Character>().transform;
        }
        CheckGround();
    }

    void Update()
    {
        Fall();
        if (isReborn) State = MState.Reborn;
        if (playerPos == null) rb.Sleep();
        else if (playerPos != null)
        {
            var distanceToPlayer = Vector2.Distance(transform.position, playerPos.position);
            if (distanceToPlayer <= agrDistance) chase = true;
            if (chase) Chase();
        }
    }

    private void Chase()
    {
        var distance = transform.position.x - playerPos.position.x;
        if (IsWall || Math.Abs(distance) < 0.05f)
        {
            rb.velocity = Vector2.zero;
            if (State != MState.Fall) State = MState.Idle;
        }
        else
        {
            rb.velocity = new Vector2(-Math.Sign(distance) * speed, 0);
            if (State != MState.Fall) State = MState.Run;
        }
        transform.localScale = new Vector2(Math.Sign(distance), 1);
    }

    private void CheckGround()
    {
        IsWall = Physics2D.OverlapCircle(WallCheck.position, 0.05F, ground);
    }

    private void Fall()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, ground))
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            State = MState.Idle;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 30f;
            State = MState.Fall;
        }
    }

    public void RebornFalse()
    {
        isReborn = false;
        bc.enabled = true;
        IsInvisable = false;
        State = MState.Idle;
    }

    public override void Die()
    {
        anim.SetInteger("State", -1);
        anim.SetTrigger("Death");
        gameObject.layer = 0;
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
        Destroy(this);
    }
}



//if (player.position.x < transform.position.x)
//{
//    if (IsWall || IsChasm || (transform.position.x - player.position.x) < 0.05f)
//    {
//        rb.velocity = Vector2.zero;
//        State = MState.Idle;
//    }
//    else
//    {
//        rb.velocity = new Vector2(-speed, 0);
//        State = MState.Run;
//    }
//    transform.localScale = new Vector2(1, 1);
//}
//else if (player.position.x > transform.position.x)
//{
//    if (IsWall || IsChasm || (player.position.x - transform.position.x) < 0.05f) rb.velocity = Vector2.zero;
//    else rb.velocity = new Vector2(speed, 0);
//    transform.localScale = new Vector2(-1, 1);
//}
