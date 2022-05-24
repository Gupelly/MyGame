using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBA : Switch
{
    public override void LoadMonsters()
    {
        var boss = Resources.Load<Boss>("Boss");
        var tomb = Resources.Load<Tomb>("Tomb");
        tomb.Radius = 20;
        tomb.spawnCount = -1;

        AddMonster(tomb, -6, 0);
        AddMonster(tomb, -2, 0);
        AddMonster(tomb, 2, 0);
        AddMonster(boss, 0, 0, -1);

    }
}
