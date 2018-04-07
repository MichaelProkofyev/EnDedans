using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingEntity : MonoBehaviour {

    private BoxCollider2D collider;
    private LayerMask blockingLayer;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        blockingLayer = LayerMask.NameToLayer("Default");
    }

    public virtual void AttemptMove(MoveDirection direction)
    {

        Vector2Int moveVector = Vector2Int.zero;
        switch (direction)
        {
            case MoveDirection.UP:
                moveVector = Vector2Int.up;
                break;
            case MoveDirection.DOWN:
                moveVector = Vector2Int.down;
                break;
            case MoveDirection.LEFT:
                moveVector = Vector2Int.left;
                break;
            case MoveDirection.RIGHT:
                moveVector = Vector2Int.right;
                break;
        }


        RaycastHit2D hit;
        bool canMove = CanMove(moveVector, out hit);

        if (canMove)
        {
            transform.position += new Vector3Int(moveVector.x, moveVector.y, 0);
        }
        return;
        if (hit.transform == null)
            return;

        //T hitComponent = hit.transform.GetComponent<T>();

        //If canMove is false and hitComponent is not equal to null, meaning MovingObject is blocked and has hit something it can interact with.
        //if (!canMove && hitComponent != null)

            //Call the OnCantMove function and pass it hitComponent as a parameter.
            //OnCantMove(hitComponent);
    }

    public bool CanMove(Vector2Int direction, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + direction;

        collider.enabled = false;
        hit = Physics2D.Linecast(start, end);
        //Re-enable boxCollider after linecast
        collider.enabled = true;

        if (hit.transform == null)
        {
            //If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
            //StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }

    //protected abstract void OnCantMove<T>(T component) where T : Component;
}
