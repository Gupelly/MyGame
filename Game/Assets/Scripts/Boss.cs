using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = System.Random;

public class Boss : Monster
{
    private GameObject[] tombs;

    public LayerMask layer;
    private CircleCollider2D attack1;
    public Transform Attack2Pos;
    private FlyingSword bulletRef;

    public Rigidbody2D rb;
    public Animator anim;

    public float DashSpeed;    
    public bool IsDashing;
    private int dashState = 3;
    private int dashDirection = 1;

    public bool CantFlip;
    public float Agrdistance = 4;
    public float AttackCooldown;
    private bool cantAttack = true;

    private bool notJumpBack = true;
    private bool notLastDash = true;
    private bool firstThrow = false;
    private bool secondThrow = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tombs = GameObject.FindGameObjectsWithTag("Respawn");
        bulletRef = Resources.Load<FlyingSword>("FlyingSword");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attack1 = GetComponent<CircleCollider2D>();
        attack1.enabled = false;
        Invoke(nameof(CanAttack), 1.5f);
    }

    private void FixedUpdate()
    {
        if (notJumpBack && Lifes == 6) IsInvisable = true;
        //if (firstThrow && secondThrow) Spawn(false);
    }

    private void Update()
    {
        var actualPlayer = player.GetComponent<Character>();
        //Spawn(false);
        if (IsDashing) Dash();
        else if (player == null) rb.Sleep();
        else if (player != null)
        {
            if (!CantFlip) Flip(-1);
            if (!cantAttack && Lifes > 6) Phase1();
            if (!cantAttack && Lifes == 6 && notJumpBack)
            {
                actualPlayer.jumpBackDirection = transform.localScale.x;
                actualPlayer.isJumpBack = true;
                ChangeDash();
                IsDashing = true;
                notJumpBack = false;
                firstThrow = true;
                IsInvisable = false;
            }
            else if (firstThrow && !actualPlayer.isJumpBack)
            {
                Spawn();
                //Invoke(nameof(Spawn), 1);
                CantFlip = true;
                anim.SetInteger("State", 4);
                firstThrow = false;
            }
            else if (Lifes == 3 && notLastDash)
            {
                if (actualPlayer.transform.position.x > 2.4 && actualPlayer.transform.position.x < 16.8) ChangeDash();
                IsDashing = true;
                secondThrow = true;
                notLastDash = false;
            }
            else if (secondThrow)
            {
                Spawn();
                CantFlip = true;
                anim.SetInteger("State", 4);
                secondThrow = false;
            }
        }
    }

    private void Phase1()
    {
        var distanceToPlayer = Math.Abs(transform.position.x - player.transform.position.x);
        if (distanceToPlayer <= Agrdistance)
        {
            CantFlip = true;
            var rnd = new Random();
            anim.SetInteger("State", rnd.Next(2) + 1);
            cantAttack = true;
            Invoke(nameof(CanAttack), 3);
        }
        else
        {
            IsDashing = true;
            cantAttack = true;
            Invoke(nameof(CanAttack), 3);
        }
    }

    public void Attack1()
    {
        attack1.enabled = !attack1.enabled;
    }

    public void TurnAround()
    {
        transform.localScale = new Vector2(-transform.localScale.x , transform.localScale.y);
    }

    public void Attack2()
    {
        var characters = Physics2D.OverlapCapsuleAll(Attack2Pos.position, new Vector2(5, 7.5f), CapsuleDirection2D.Vertical, 0);
        foreach (var character in characters)
            character?.GetComponent<Character>()?.ReceiveDamage();
    }

    private void ChangeDash()
    {
        dashState = dashState == 3 ? 5 : 3;
        dashDirection = dashDirection == 1 ? -1 : 1;
    }

    private void Dash()
    {
        anim.SetInteger("State", dashState);
        if (transform.localScale.x > 0) rb.velocity = new Vector2(dashDirection * DashSpeed, rb.velocity.y);
        else rb.velocity = new Vector2(-dashDirection * DashSpeed, rb.velocity.y);
    }
    public void Shoot()
    {
        var position = new Vector2(transform.position.x + 2 * transform.localScale.x, transform.position.y + 1.4f);
        var newBullet = Instantiate(bulletRef, position, bulletRef.transform.rotation, gameObject.transform.parent);
        newBullet.Direction = newBullet.transform.right * transform.localScale.x;
    }

    private void Spawn()
    {
        foreach (var tomb in tombs)
        {
            var realTomb = tomb.GetComponent<Tomb>();
            if (realTomb != null) realTomb.SpawnOnCommand();
        }
    }

    private void CanAttack()
    {
        cantAttack = false;
    }

    public void StopAttack()
    {
        CantFlip = false;
        anim.SetInteger("State", 0);
    }

    public override void Die()
    {
        anim.SetTrigger("Die");
        gameObject.layer = 0;
        Destroy(this);
    }

}
