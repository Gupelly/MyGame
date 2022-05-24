using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSword : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    public LayerMask ground;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        if (Physics2D.OverlapCircle(transform.position, 0.25f, ground)) direction *= new Vector2(-1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var unit = collision.GetComponent<Alive>();
        if (unit is Character) unit.ReceiveDamage();
        if (unit is Boss)
        {
            var boss = unit.GetComponent<Boss>();
            boss.CantFlip = false;
            boss.StopAttack();
            Destroy(gameObject);
        }
    }
}
