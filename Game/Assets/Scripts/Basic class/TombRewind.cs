using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombRewind : Rewind
{
    public override void DoRewind()
    {
        if (!gameObject.TryGetComponent<Tomb>(out var moveMonster))
            gameObject.AddComponent<Tomb>();
    }
}
