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

    public void AttemptMove(MoveDirection direction)
    {
        Vector2Int moveVector = DirectionToVector(direction);
        RaycastHit2D hit;
        bool canMove = CanMove(moveVector, out hit);

        if (canMove)
        {
            transform.position += new Vector3Int(moveVector.x, moveVector.y, 0);
            //Wrap movement around board
            int wrappedXPos = Wrap((int)transform.position.x, BoardManager.Instance.bottomLeftCorner.x, BoardManager.Instance.topRightCorner.x + 1);
            int wrappedYPos = Wrap((int)transform.position.y, BoardManager.Instance.bottomLeftCorner.y, BoardManager.Instance.topRightCorner.y + 1);
            transform.position = new Vector3(wrappedXPos, wrappedYPos, 0);
        }
        else if(hit.transform != null)
        {
            OnCantMove(hit.collider.gameObject, direction);
        }
    }

    protected virtual void OnCantMove(GameObject blocker, MoveDirection direction)
    {
        //Try getting the wrapper
        Wrapper possibleWrapper = blocker.GetComponent<Wrapper>();
        if (possibleWrapper != null)
        {
            var possibleBlocker = possibleWrapper.GetBlocker(direction);
            if (possibleBlocker != null)
            {
                OnCantMove(possibleBlocker, direction);
            }
            else
            {
                //TODO: Replace with getting position form pared wrapper
                Vector2Int moveVector = DirectionToVector(direction);
                transform.position += new Vector3Int(moveVector.x, moveVector.y, 0);
                //Wrap movement around board
                int wrappedXPos = Wrap((int)transform.position.x, BoardManager.Instance.bottomLeftCorner.x, BoardManager.Instance.topRightCorner.x + 1);
                int wrappedYPos = Wrap((int)transform.position.y, BoardManager.Instance.bottomLeftCorner.y, BoardManager.Instance.topRightCorner.y + 1);
                transform.position = new Vector3(wrappedXPos, wrappedYPos, 0);
            }
        }
    }

    private bool CanMove(Vector2Int direction, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + direction;

        collider.enabled = false;
        hit = Physics2D.Linecast(start, end);
        //Re-enable boxCollider after linecast
        collider.enabled = true;

        if (hit.transform == null)
        {
            return true;
        }
        return false;
    }

    private Vector2Int DirectionToVector(MoveDirection direction)
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
        return moveVector;
    }

    private int Wrap(int x, int x_min, int x_max)
    {
        if (x < x_min)
            return x_max - (x_min - x) % (x_max - x_min);
        else
            return x_min + (x - x_min) % (x_max - x_min);
    }
}
