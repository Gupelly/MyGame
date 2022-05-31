using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var unit = collision.GetComponent<Boss>();
        if (unit != null && unit is Boss)
        {
            unit.rb.velocity = Vector2.zero;
            unit.IsDashing = false;
            if (unit.anim.GetInteger("State") == 3 || unit.anim.GetInteger("State") == 5) unit.StopAttack();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var unit = collision.GetComponent<Boss>();
        if (unit != null && unit.anim.GetInteger("State") == 5)
            if ((unit.transform.position.x < 3 && unit.transform.localScale.x == 1)
            || (unit.transform.position.x > 16 && unit.transform.localScale.x == -1))
            {
                unit.rb.velocity = Vector2.zero;
                unit.IsDashing = false;
                unit.StopAttack();
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        if (character != null && character is Character)
        {
            if (character.isJumpBack) character.rb.velocity = Vector2.zero;
            character.isJumpBack = false;
        }
    }
}
