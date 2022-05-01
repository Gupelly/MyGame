using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMonster : Monster
{
    [SerializeField]
    private float rate = 2f;

    private Bullet bulletRef;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        bulletRef = Resources.Load<Bullet>("Bullet");
        InvokeRepeating("Shoot", rate, rate);
    }

    private void Start()
    {      
        //InvokeRepeating("Shoot", rate, rate);
    }

    private void Shoot()
    {
        var position = transform.position;
        position.y += 0.7f;
        var newBullet = Instantiate(bulletRef, position, bulletRef.transform.rotation);
        newBullet.Direction = -newBullet.transform.right * transform.localScale.x;
    }

    public override void Die()
    {
        anim.SetTrigger("Death");
        gameObject.layer = 0;
        Destroy(this);
    }
}
