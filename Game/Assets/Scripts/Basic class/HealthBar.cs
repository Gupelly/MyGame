using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform[] hearts = new Transform[5];
    private Character character;

    private void Awake()
    {
        Spawn();
    }

    private void FixedUpdate()
    {
        for (var i = 0; i < hearts.Length; i++)
        {
            if (i < character?.Lifes) hearts[i].gameObject.SetActive(true);
            else hearts[i].gameObject.SetActive(false);
        }
        if (character?.Lifes == 0) Invoke(nameof(Spawn), 2f);
    }

    private void Spawn()
    {
        character = FindObjectOfType<Character>();
        for (var i = 0; i < hearts.Length; i++)
            hearts[i] = transform.GetChild(i);
    }
}
