using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform[] hearts = new Transform[5];
    private Character character;

    private void Awake()
    {
        character = FindObjectOfType<Character>();
        for (var i = 0; i < hearts.Length; i++)
            hearts[i] = transform.GetChild(i);
    }

    private void Update()
    {
        for (var i = 0; i < hearts.Length; i++)
        {
            if (i < character.Lifes) hearts[i].gameObject.SetActive(true);
            else hearts[i].gameObject.SetActive(false);
        }
    }
}
