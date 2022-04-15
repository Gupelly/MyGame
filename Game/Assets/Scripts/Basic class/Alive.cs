using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alive : MonoBehaviour
{
    public static int Lifes;
    public bool IsAlive = Lifes > 0;

    public virtual void GetDamage()
    {
        Lifes--;
        WhenGetdamage();
        if (Lifes == 0) Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void WhenGetdamage()
    {

    }
}
