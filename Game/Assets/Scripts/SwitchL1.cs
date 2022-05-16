using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchL1 : Switch
{
    public override void LoadMonsters()
    {
        var skeleton = Resources.Load<MoveMonster>("MoveMonster");
        var goblin = Resources.Load<ShootMonster>("ShootMonster");
        var tomb = Resources.Load<Tomb>("Tomb");

        AddMonster(skeleton, -23, -6, -1);
        AddMonster(skeleton, -28, -6, -1);
        AddMonster(goblin, -34, -3.2f, -1);
        AddMonster(goblin, -34, -0.2f, -1);
        AddMonster(goblin, -34, 2.8f, -1);
        AddMonster(tomb, -32, 2.9f);
    }
}
