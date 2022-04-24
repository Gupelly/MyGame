using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Alive
{
    [SerializeField]
    private float speed = 5.0f;
    private Vector3 direction;
    private bool isReflected = false;
    public Vector3 Direction { set { direction = value; } }

    public LayerMask ground;
    public LayerMask monster;

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        Lifes = 2;
    }

    private void Start()
    {
        Invoke("Destroy", 5f);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        if (Physics2D.OverlapCircle(transform.position, 0.25f, ground)) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Alive unit = collision.GetComponent<Alive>();
        if (unit is Character && !isReflected)
        {
            Debug.Log(true);
            unit.ReceiveDamage();
            Destroy(gameObject);
        }
        if (unit is Monster && isReflected)
        {
            Debug.Log(true);
            unit.ReceiveDamage(2);
            Destroy(gameObject);
        }
    }

    public override void WhenReceiveDamage()
    {
        isReflected = true;
        direction *= new Vector2(-1, 1);
        sprite.color = Color.green;
    }

    private void Destroy()
    {
        if (!isReflected) 
            Destroy(gameObject);
    }
}
