using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMonsterRewind : Rewind
{
    public override void DoRewind()
    {
        if (!gameObject.TryGetComponent<ShootMonster>(out var moveMonster))
        {
            Debug.Log(true);
            gameObject.AddComponent<ShootMonster>();
        }
    }
}
