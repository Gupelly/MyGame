using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMonster : Monster
{
    [SerializeField]
    private float rate = 2f;

    private Bullet bulletRef;

    private void Awake()
    {
        bulletRef = Resources.Load<Bullet>("Bullet");
    }

    private void Start()
    {      
        InvokeRepeating("Shoot", rate, rate);
    }

    private void Shoot()
    {
        var position = transform.position;
        position.y += 0.5f;
        var newBullet = Instantiate(bulletRef, position, bulletRef.transform.rotation);
        newBullet.Direction = -newBullet.transform.right * transform.localScale.x;
    }

}
