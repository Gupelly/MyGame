using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveMonster : Monster
{
    [SerializeField]
    private float agrDistance = 4.0f;

    public LayerMask ground;

    public Transform WallCheck;
    private bool IsWall;

    public LayerMask Player;
    private Transform player;
    private bool chase = false;
    public bool isReborn;

    private Rigidbody2D rb;
    private Animator anim;
    

    private enum MState
    {
        Idle,
        Run,
        Reborn,
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
    }

    private void FixedUpdate()
    {
        //if (speed == 0) anim.SetTrigger("auch");
        if (Physics2D.OverlapCircle(transform.position, agrDistance, Player))
        {
            var newPlayer = Physics2D.OverlapCircle(transform.position, agrDistance, Player);
            player = newPlayer.GetComponent<Character>().transform;
        }
        CheckGround();
    }

    void Update()
    {
        if (isReborn) State = MState.Reborn;
        else if (player == null) rb.Sleep();
        else if (player != null)
        {
            var distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= agrDistance) chase = true;
            if (chase) Chase();
        }
    }

    private void Chase()
    {
        var distance = transform.position.x - player.position.x;
        if (IsWall || Math.Abs(distance) < 0.05f)
        {
            rb.velocity = Vector2.zero;
            State = MState.Idle;
        }
        else
        {
            rb.velocity = new Vector2(-Math.Sign(distance) * speed, 0);
            State = MState.Run;
        }
        transform.localScale = new Vector2(Math.Sign(distance), 1);
    }

    private void CheckGround()
    {
        IsWall = Physics2D.OverlapCircle(WallCheck.position, 0.05F, ground);
    }

    public void RebornFalse()
    {
        isReborn = false;
        IsInvisable = false;
        State = MState.Idle;
    }

    public override void Die()
    {
        anim.SetInteger("State", -1);
        anim.SetTrigger("Death");
        gameObject.layer = 0;
        rb.velocity = Vector2.zero;
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
