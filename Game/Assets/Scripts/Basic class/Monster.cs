using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Alive
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Alive unit = collision.GetComponent<Alive>();
        if (unit && unit is Character)
            unit.ReceiveDamage();
    }
}
