using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomb : MonoBehaviour
{
    [SerializeField]
    protected float radius = 2.5f;
    [SerializeField]
    protected int spawnCount = 3;
    [SerializeField]
    protected float respawnTime = 5f;

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
        if (Physics2D.OverlapCircle(transform.position, radius, character) && !active)
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
            newEnemy.transform.position = transform.position + new Vector3(0, 1.2f);
            newEnemy.isReborn = true;
            newEnemy.IsInvisable = true;
            spawnCount--;
            Invoke("Spawn", respawnTime);
        }
    }
}
