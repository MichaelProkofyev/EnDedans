using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonComponent<Player> {


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
        if (parts.Count != 0)
        {
            SetPartSelected(parts[0]);
        }
	}

    private void Update()
    {
        //Selection
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
            selectedPart.HandleInputDirection(InputDirection.UP);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedPart.HandleInputDirection(InputDirection.DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedPart.HandleInputDirection(InputDirection.LEFT);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedPart.HandleInputDirection(InputDirection.RIGHT);
        }
    }

    //Transform into "makeAction" ?
    public void Move(Vector2 direction)
    {
        transform.position += (Vector3)direction;
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
