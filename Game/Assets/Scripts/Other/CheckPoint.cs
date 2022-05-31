using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var unit = collision.GetComponent<Character>();
        unit.Lifes = 3;
        unit.transform.position = new Vector3(-4 + 1.776685f, -1);
    }
}