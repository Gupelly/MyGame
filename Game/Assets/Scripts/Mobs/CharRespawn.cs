using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharRespawn : MonoBehaviour
{
    public Vector3 coordinates;
    private Character character;
    private bool unlockDoubleJump = false;
    private bool unlockDash = false;
    private bool stop;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void FixedUpdate()
    {
        unlockDoubleJump = character.unlockDoubleJump;
        unlockDash = character.unlockDash;
        if (character.Lifes == 0 && !stop)
        {
            Invoke(nameof(Respawn), 2f);
            stop = true;
        }
    }
    private void Respawn()
    {
        gameObject.AddComponent<Character>();
        character = GetComponent<Character>();

        transform.position = coordinates;
        transform.localScale = new Vector3(1, 1);
        character.unlockDash = unlockDash;
        character.unlockDoubleJump = unlockDoubleJump;
        gameObject.layer = 3;
        stop = false;
    }
}
