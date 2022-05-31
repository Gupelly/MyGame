using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMonster : Monster
{
    private Bullet bulletRef;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        bulletRef = Resources.Load<Bullet>("Bullet");
    }

    public void Shoot()
    {
        var position = transform.position;
        position.y += 0.8f;
        var newBullet = Instantiate(bulletRef, position, bulletRef.transform.rotation, gameObject.transform.parent);
        newBullet.Direction = -newBullet.transform.right * transform.localScale.x;
    }

    public override void Die()
    {
        anim.SetTrigger("Death");
        gameObject.layer = 0;
        Destroy(this);
    }
}
