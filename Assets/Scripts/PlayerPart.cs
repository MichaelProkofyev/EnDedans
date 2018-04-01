using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public abstract class PlayerPart : MonoBehaviour
{
    [SerializeField] SpriteRenderer partSprite;

    public void SetSelected(bool isSelected)
    {
        if (isSelected)
        {
            partSprite.color = Color.red;
        }
        else
        {
            partSprite.color = Color.white;
        }
    }

    public abstract void HandleInputDirection(InputDirection direction);
}
