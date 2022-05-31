using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchR1 : Switch
{
    public override void LoadMonsters()
    {
        var skeleton = Resources.Load<MoveMonster>("MoveMonster");
        var fly = Resources.Load<FlyingEye>("FlyingEye");

        AddMonster(skeleton, -6.5f, 0.2f);
        AddMonster(skeleton, -0.5f, 0.2f);
        AddMonster(fly, 0, 3.5f);
        AddMonster(fly, 3, 5f);
        AddMonster(fly, 6.5f, 7);
    }
}
