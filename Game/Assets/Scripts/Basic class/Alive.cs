using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alive : MonoBehaviour
{
    public int Lifes;

    public void ReceiveDamage(int damage = 1)
    {
        Lifes -= damage;
        WhenReceiveDamage();
        if (Lifes == 0) Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public virtual void WhenReceiveDamage()
    {
        Debug.Log(Lifes);
    }
}
