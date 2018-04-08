using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : SingletonComponent<GameController> {

    public enum GameState
    {
        PLAYER_TURN,
        ENEMIES_TURN
    }

    public GameState state;
    public List<Enemy> enemies = new List<Enemy>();


    public int Score
    {
        set
        {
            score = value;
            scoreText.text = score.ToString();
        }
        get { return score; }
    }

    [SerializeField] private Text scoreText;

    private int score;

    // Use this for initialization
    void Start()
    {
        Player.Instance.OnMoved += OnPlayerMoved;
        state = GameState.PLAYER_TURN;

        BoardManager.Instance.RefreshBoard();
    }

    public void TreasureCollected()
    {
        Score += 10;
        BoardManager.Instance.RefreshBoard();
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
