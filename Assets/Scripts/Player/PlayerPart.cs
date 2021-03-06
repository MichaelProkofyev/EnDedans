﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerPart : MovingEntity
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

    protected override void OnCantMove(GameObject blocker, MoveDirection direction)
    {
        base.OnCantMove(blocker, direction);
    }


    public abstract void HandleInputDirection(MoveDirection direction);
}
