using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchU1 : Switch
{
    public override void LoadMonsters()
    {
        var wolf = Resources.Load<Wolf>("Wolf");
        AddMonster(wolf, -6, 3);
        AddMonster(wolf, 9, 6);
    }
}
