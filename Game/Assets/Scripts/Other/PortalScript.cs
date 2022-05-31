using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Alive unit = collision.GetComponent<Character>();
        if (unit is Character)
        {
            unit.WhenReceiveDamage();
            unit.IsInvisable = false;
        }
    }
}
