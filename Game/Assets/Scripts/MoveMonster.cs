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

    public Transform ChasmCheck;
    private bool IsChasm;

    public LayerMask Player;
    private Transform player;
    private bool chase = false;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(transform.position, agrDistance, Player))
        {
            var newPlayer = Physics2D.OverlapCircle(transform.position, agrDistance, Player);
            player = newPlayer.GetComponent<Character>().transform;
        }
        CheckGround();
    }

    void Update()
    {
        if (player == null) rb.Sleep();
        if (player != null)
        {
            var distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= agrDistance) chase = true;
            if (chase) Chase();
        }
    }

    private void Chase()
    {
        if (player.position.x < transform.position.x)
        {
            if (IsWall || IsChasm) rb.velocity = Vector2.zero;
            else rb.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else if (player.position.x > transform.position.x)
        {
            if (IsWall || IsChasm) rb.velocity = Vector2.zero;
            else rb.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
    }

    private void CheckGround()
    {
        IsWall = Physics2D.OverlapCircle(WallCheck.position, 0.05F, ground);
        IsChasm = !Physics2D.OverlapCircle(ChasmCheck.position, 0.1F, ground);
    }
}
