using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomb : MonoBehaviour
{
    public float Radius = 2.5f;
    public int spawnCount = 3;
    public float respawnTime = 5f;

    public LayerMask character;
    protected MoveMonster enemyRef;
    private SpriteRenderer sprite;

    protected bool active = false;

    private void Awake()
    {
        enemyRef = Resources.Load<MoveMonster>("MoveMonster");
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (spawnCount == 0) Destroy(sprite);
        if (Physics2D.OverlapCircle(transform.position, Radius, character) && !active)
        {
            Spawn();
            active = true;
        }
    }

    protected void Spawn()
    {
        if (spawnCount > 0)
        {
            var newEnemy = Instantiate(enemyRef, gameObject.transform);
            newEnemy.transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
            newEnemy.isReborn = true;
            newEnemy.IsInvisable = true;
            newEnemy.bc.enabled = false;
            newEnemy.agrDistance = Radius;
            spawnCount--;
            Invoke("Spawn", respawnTime);
        }
    }

    public void SpawnOnCommand()
    {
        var newEnemy = Instantiate(enemyRef, gameObject.transform);
        newEnemy.transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
        newEnemy.isReborn = true;
        newEnemy.IsInvisable = true;
        newEnemy.bc.enabled = false;
        newEnemy.agrDistance = Radius;
    }
}
