using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class Monster : Alive
{
    public float speed;
    private float constSpeed;
    public float stanTime;

    private void Start()
    {
        constSpeed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Alive unit = collision.GetComponent<Character>();
        if (unit is Character)
            unit.ReceiveDamage();
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
