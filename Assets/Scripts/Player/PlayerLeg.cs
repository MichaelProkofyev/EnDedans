using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeg : PlayerPart
{
    public override void HandleInputDirection(MoveDirection direction)
    {
        Player.Instance.MoveBody(direction);
    }

    protected override void OnCantMove(GameObject blocker)
    {
        print("Can't move because of " + blocker.name);
    }
}
