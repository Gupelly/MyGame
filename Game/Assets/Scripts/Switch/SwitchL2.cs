using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchL2 : Switch
{
    public override void LoadMonsters()
    {
        var goblin = Resources.Load<ShootMonster>("ShootMonster");
        var fly = Resources.Load<FlyingEye>("FlyingEye");

        AddMonster(goblin, 0, -4);
        AddMonster(fly, -10, -1);
        AddMonster(fly, -17, -1);
        AddMonster(fly, -23.5f, -1);
    }
}
