﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum MoveDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class Player : SingletonComponent<Player> {

    public Action OnMoved = () => { };

    private Camera camera;
    private List<PlayerPart> parts = new List<PlayerPart>();
    private PlayerPart selectedPart;

    protected override void Awake () {
        base.Awake();
        camera = Camera.main;

        foreach (var part in GetComponentsInChildren<PlayerPart>())
        {
            parts.Add(part);
        }
        foreach (var part in parts)
        {
            PlayerLeg possibleLeg = part as PlayerLeg;
            if (possibleLeg != null)
            {
                SetPartSelected(possibleLeg);
                break;
            }
        }
	}

    private void Update()
    {
        if (GameController.Instance.state != GameController.GameState.PLAYER_TURN)
        {
            return;
        }

        //Parts selection
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                var playerPart = hit.collider.GetComponent<PlayerPart>();
                if (playerPart != null)
                {
                    SetPartSelected(playerPart);
                }
            }
        }

        //Movement
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            HandleInputDirection(MoveDirection.UP);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            HandleInputDirection(MoveDirection.DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            HandleInputDirection(MoveDirection.LEFT);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            HandleInputDirection(MoveDirection.RIGHT);
        }
    }

    private void HandleInputDirection(MoveDirection direction)
    {
        selectedPart.HandleInputDirection(direction);
        OnMoved();
    }

    public void MoveBody(MoveDirection direction)
    {
        var directionSortedParts = parts.OrderByDescending(p => 
        {
            Vector2 partPosition = p.transform.position;
            switch (direction)
            {
                case MoveDirection.UP:
                    return partPosition.y;
                case MoveDirection.DOWN:
                    return -partPosition.y;
                case MoveDirection.LEFT:
                    return -partPosition.x;
                case MoveDirection.RIGHT:
                    return partPosition.x;
                default:
                    return 0;
            }
        });
        foreach (var part in directionSortedParts)
        {
            part.AttemptMove(direction);
        }
    }

    private void SetPartSelected(PlayerPart newSelectedPart)
    {
        if (newSelectedPart == selectedPart)
        {
            return;
        }

        if (selectedPart != null)
        {
            //Deselect prev part
            selectedPart.SetSelected(false);
        }
        selectedPart = newSelectedPart;
        selectedPart.SetSelected(true);
    }
}
