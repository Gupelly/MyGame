using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diam : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var unit = collision.GetComponent<Character>();
        unit.unlockDoubleJump = true;
        Destroy(gameObject);
    }
}
