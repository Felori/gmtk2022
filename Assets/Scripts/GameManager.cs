using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameMap gameMap = default;
    [SerializeField] ActionPointsUI actionPointsUi = default;
    [SerializeField] CinemachineVirtualCamera playerCam = default;

    [SerializeField] UnityEvent onPlayerWon = default;
    [SerializeField] UnityEvent onPlayerLost = default;

    Player player;
    List<Enemy> enemies;

    int[] actionPoints = new int[3];

    int foodTemp;

    bool gameOver;

    int CurrentActionPoints
    {
        get => actionPoints[0];
        set
        {
            actionPoints[0] = value;
            actionPointsUi.SetCurrentActionPoints(value);
        }
    }

    void StartGame()
    {
        enemies = new List<Enemy>();
        Character[] characters = gameMap.OnGameStarted();
        foreach(Character character in characters)
        {
            if (character is Player player) this.player = player;
            if (character is Enemy enemy)
            {
                enemy.onDied += () => enemies.Remove(enemy);
                enemies.Add(enemy);
            }
        }

        if (player == null)
        {
            Debug.LogError("No player found in the map!");
            return;
        }

        player.onPlayerDied += OnPlayerDied;

        playerCam.Follow = player.transform;

        actionPoints[0] = RollDice();
        actionPoints[1] = RollDice();
        actionPoints[2] = RollDice();

        actionPointsUi.SetActionPoints(actionPoints, false);

        foodTemp = gameMap.MaxMoves;

        gameOver = false;
    }

    void RestartGame()
    {
        gameMap.ResetMap();

        StartGame();
    }

    void EndTurn()
    {
        foreach(Enemy enemy in enemies)
        {
            enemy.DoEnemyTurn();
        }

        actionPoints[0] = actionPoints[1];
        actionPoints[1] = actionPoints[2];
        actionPoints[2] = RollDice();

        actionPointsUi.SetActionPoints(actionPoints, true);
    }

    void MovePlayer(int x, int y)
    {
        if (CurrentActionPoints == 0) return;

        GameTile tile = gameMap.GetTile(x, y);
        if (tile != null)
        {
            player.LookAt(tile.transform.position);

            if (tile.Character != null && tile.Character is Enemy enemy)
            {
                player.Attack(enemy, CurrentActionPoints);
                CurrentActionPoints = 0;
            }
            else
            {
                player.SetTile(tile);
                CurrentActionPoints--;
            }

            foodTemp--;
            if (foodTemp == 0)
            {
                PlayerLose();
                Debug.Log("Food got too cold!");
            }

            if (CurrentActionPoints == 0) EndTurn();
        }
    }

    int RollDice()
    {
        return Random.Range(1, 7);
    }

    void PlayerWin()
    {
        Debug.Log("Player Won!");
        gameOver = true;
        onPlayerWon?.Invoke();
    }

    void PlayerLose()
    {
        gameOver = true;
        onPlayerLost?.Invoke();
    }

    void OnPlayerDied()
    {
        PlayerLose();
        Debug.Log("Player Died!");
    }

    void OnPlayerEnteredFinishLine()
    {
        if(enemies.Count == 0)
            PlayerWin();
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) RestartGame();

        if (gameOver || player == null) return;

        (int x, int y) = player.Tile.Position;

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MovePlayer(x, y + 1);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePlayer(x - 1, y);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MovePlayer(x, y - 1);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovePlayer(x + 1, y);
        }
    }

    private void Awake()
    {
        FinishLine.onPlayerEnteredFinishLine += OnPlayerEnteredFinishLine;
    }

    private void OnDisable()
    {
        FinishLine.onPlayerEnteredFinishLine -= OnPlayerEnteredFinishLine;
    }
}
