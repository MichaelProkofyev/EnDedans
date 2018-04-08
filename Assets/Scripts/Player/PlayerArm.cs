using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArm : PlayerPart
{
    public override void HandleInputDirection(MoveDirection direction)
    {
        AttemptMove(direction);
    }

    protected override void OnCantMove(GameObject blocker, MoveDirection direction)
    {
        base.OnCantMove(blocker, direction);
        //Try damaging
        IDamagable damagableBlocker = blocker.GetComponent<IDamagable>();
        if (damagableBlocker != null)
        {
            damagableBlocker.Damage();
        }
    }
}
