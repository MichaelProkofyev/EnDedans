﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingEntity, IDamagable
{
    public int hp = 3;

    public void Act()
    {
        MoveDirection movementDirection;

        var playerEye = GameObject.Find("Eye");
        if (playerEye != null)
        {
            Vector2Int target = new Vector2Int((int)playerEye.transform.position.x, (int)playerEye.transform.position.y);

            if (Mathf.Abs(target.x - transform.position.x) < float.Epsilon)

                //If the y coordinate of the target's (player) position is greater than the y coordinate of this enemy's position set y direction 1 (to move up). If not, set it to -1 (to move down).
                movementDirection = target.y > transform.position.y ? MoveDirection.UP : MoveDirection.DOWN;

            //If the difference in positions is not approximately zero (Epsilon) do the following:
            else
                movementDirection = target.x > transform.position.x ? MoveDirection.RIGHT : MoveDirection.LEFT;

            //Call the AttemptMove function and pass in the generic parameter Player, because Enemy is moving and expecting to potentially encounter a Player
            AttemptMove(movementDirection);
        }
    }

    public void Damage()
    {
        hp--;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnCantMove(GameObject blocker, MoveDirection direction)
    {
        base.OnCantMove(blocker, direction);

        var possiblePlayerLeg = blocker.GetComponent<PlayerLeg>();
        if (possiblePlayerLeg != null)
        {
            GameController.Instance.Restart();
        }

        print("ENEMY BLOCKED BY " + blocker);
    }

}