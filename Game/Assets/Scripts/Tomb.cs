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

    protected bool active = false;

    private void Awake()
    {
        enemyRef = Resources.Load<MoveMonster>("MoveMonster");
    }

    private void Update()
    {
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
            var newEnemy = Instantiate(enemyRef);
            newEnemy.transform.position = transform.position;
            spawnCount--;
            Invoke("Spawn", respawnTime);
        }
    }
}
