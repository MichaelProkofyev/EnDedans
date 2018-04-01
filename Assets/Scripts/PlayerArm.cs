using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArm : PlayerPart
{
    public override void HandleInputDirection(InputDirection direction)
    {
        Vector2 movementVector = Vector2.zero;

        switch (direction)
        {
            case InputDirection.UP:
                movementVector = Vector2.up;
                break;
            case InputDirection.DOWN:
                movementVector = Vector2.down;
                break;
            case InputDirection.LEFT:
                movementVector = Vector2.left;
                break;
            case InputDirection.RIGHT:
                movementVector = Vector2.right;
                break;
        }
        Move(movementVector);
    }

    private void Move(Vector2 direction)
    {
        transform.position += (Vector3)direction;
    }
}
