using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonComponent<GameController> {

    public enum GameState
    {
        PLAYER_TURN,
        ENEMIES_TURN
    }

    public GameState state;

    public List<Enemy> enemies = new List<Enemy>();

    // Use this for initialization
    void Start()
    {
        Player.Instance.OnMoved += OnPlayerMoved;
        state = GameState.PLAYER_TURN;
    }

    void OnPlayerMoved()
    {
        state = GameState.ENEMIES_TURN;
        //Move enemies

        foreach (var enemy in enemies)
        {
            enemy.Act();
        }

        state = GameState.PLAYER_TURN;
    }
}
