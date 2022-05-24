using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class Monster : Alive
{
    public GameObject player;
    public float speed;
    private float constSpeed;
    public float stanTime;

    private void Start()
    {
        constSpeed = speed;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Alive unit = collision.GetComponent<Character>();
    //    if (unit is Character)
    //        unit.ReceiveDamage();
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        Alive unit = collision.GetComponent<Character>();
        if (unit is Character)
            unit.ReceiveDamage();
    }
    protected void Flip(int sign = 1)
    {
        var distance = transform.position.x - player.transform.position.x;
        transform.localScale = new Vector2(sign * Math.Sign(distance), 1);
    }

    public override void WhenReceiveDamage()
    {
        Debug.Log(Lifes);
        speed = 0;
        Invoke("WakeUp", stanTime);
    }

    private void WakeUp()
    {
        speed = constSpeed;
    }
}
