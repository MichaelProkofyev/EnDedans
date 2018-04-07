using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingEntity, IDamagable
{
    public int hp = 3;

    // Use this for initialization
    void Start()
    {
        GameController.Instance.enemies.Add(this);
    }

    public void Act()
    {
        MoveDirection movementDirection;

        Vector2Int target = Vector2Int.zero;

        if (Mathf.Abs(target.x - transform.position.x) < float.Epsilon)

            //If the y coordinate of the target's (player) position is greater than the y coordinate of this enemy's position set y direction 1 (to move up). If not, set it to -1 (to move down).
            movementDirection = target.y > transform.position.y ? MoveDirection.UP : MoveDirection.DOWN;

        //If the difference in positions is not approximately zero (Epsilon) do the following:
        else
            movementDirection = target.x > transform.position.x ? MoveDirection.RIGHT : MoveDirection.LEFT;

        //Call the AttemptMove function and pass in the generic parameter Player, because Enemy is moving and expecting to potentially encounter a Player
        AttemptMove(movementDirection);
    }

    public void Damage()
    {
        hp--;
        if (hp <= 0)
        {
            GameController.Instance.enemies.Remove(this);
            Destroy(gameObject);
        }
    }

    protected override void OnCantMove(GameObject blocker)
    {
        print("ENEMY BLOCKED BY " + blocker);
    }

}