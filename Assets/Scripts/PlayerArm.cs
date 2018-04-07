using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArm : PlayerPart
{
    public override void HandleInputDirection(MoveDirection direction)
    {
        AttemptMove(direction);
    }
}
