using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alive : MonoBehaviour
{
    public int Lifes;
    public bool IsInvisable = false;
    public bool IsDead = false;

    public void ReceiveDamage(int damage = 1)
    {
        if (IsInvisable) return;
        Lifes -= damage;
        Effect();
        if (Lifes <= 0) Die();
        WhenReceiveDamage();       
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void WhenReceiveDamage()
    {
        Debug.Log(Lifes);
    }

    public virtual void Effect()
    {

    }
}
