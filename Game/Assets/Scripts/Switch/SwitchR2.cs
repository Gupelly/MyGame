using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchR2 : Switch
{
    public override void LoadMonsters()
    {
        var fly = Resources.Load<FlyingEye>("FlyingEye");
        var wolf = Resources.Load<Wolf>("Wolf");

        AddMonster(wolf, -8, 0);
        AddMonster(wolf, 0, 0);
        AddMonster(fly, -3, 2);
    }
}
