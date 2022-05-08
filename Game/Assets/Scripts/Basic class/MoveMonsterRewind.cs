using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMonsterRewind : Rewind
{
    public override void DoRewind()
    {
        if (!gameObject.TryGetComponent<MoveMonster>(out var moveMonster))
            gameObject.AddComponent<MoveMonster>();
    }

}
