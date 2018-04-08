using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrapper : MonoBehaviour {

    [SerializeField] private Wrapper linkedWrapper;
    private BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    public GameObject GetBlocker(MoveDirection direction)
    {
        Vector2Int checkDirection = Vector2Int.zero;
        switch (direction)
        {
            case MoveDirection.UP:
                checkDirection = Vector2Int.up;
                break;
            case MoveDirection.DOWN:
                checkDirection = Vector2Int.down;
                break;
            case MoveDirection.LEFT:
                checkDirection = Vector2Int.left;
                break;
            case MoveDirection.RIGHT:
                checkDirection = Vector2Int.right;
                break;
        }

        RaycastHit2D hit;
        bool directionFree = linkedWrapper.DirectionFree(checkDirection, out hit);

        if(hit.transform != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    public bool DirectionFree(Vector2Int direction, out RaycastHit2D hit)
    {
        Vector2 start = transform.transform.position;
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
}
