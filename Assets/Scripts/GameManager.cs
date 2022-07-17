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
    [SerializeField] UnityEvent<float> onFoodTempChanged = default;
    [SerializeField] UnityEvent<int> onPlayerHealthChanged = default;

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
            player.SetRemainingActionPoints(value);
            actionPointsUi.SetCurrentActionPoints(value);
        }
    }

    Queue<GameAction> actionQueue;
    GameAction currentAction;

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
        player.onHealthChanged += OnPlayerHealthChanged;

        playerCam.Follow = player.transform;

        actionPoints[0] = RollDice();
        actionPoints[1] = RollDice();
        actionPoints[2] = RollDice();

        actionPointsUi.SetActionPoints(actionPoints, false);

        player.SetRemainingActionPoints(CurrentActionPoints);

        foodTemp = gameMap.MaxMoves;
        onFoodTempChanged?.Invoke(1f);

        gameOver = false;

        actionQueue = new Queue<GameAction>();
    }

    void RestartGame()
    {
        gameMap.ResetMap();

        StartGame();
    }

    void StartTurn()
    {
        if (gameOver) return;

        actionPoints[0] = actionPoints[1];
        actionPoints[1] = actionPoints[2];
        actionPoints[2] = RollDice();

        actionPointsUi.SetActionPoints(actionPoints, true);

        player.SetRemainingActionPoints(CurrentActionPoints);
    }

    void MovePlayer(int x, int y)
    {
        if (CurrentActionPoints == 0) return;

        GameTile tile = gameMap.GetTile(x, y);
        if (tile != null)
        {
            if (tile.Character != null && tile.Character is Enemy enemy)
            {
                QueueAction(new CharacterAttackAction(player, enemy, CurrentActionPoints));
                CurrentActionPoints = 0;
            }
            else
            {
                QueueAction(new PlayerMoveAction(player, player.transform.position, tile.transform.position));
                player.SetTile(tile);
                CurrentActionPoints--;
            }

            foodTemp--;
            onFoodTempChanged?.Invoke(foodTemp * 1f / gameMap.MaxMoves);
            if (foodTemp == 0)
            {
                PlayerLose();
                Debug.Log("Food got too cold!");
            }

            if (CurrentActionPoints == 0)
            {
                QueueAction(new EndTurnAction(enemies));
                QueueAction(new ControlAction(StartTurn));
            }
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

    void OnPlayerHealthChanged(int health)
    {
        onPlayerHealthChanged?.Invoke(health);
    }

    void QueueAction(GameAction action)
    {
        actionQueue.Enqueue(action);
    }

    bool ProcessActions()
    {
        if(currentAction != null)
        {
            if(currentAction.Done)
            {
                currentAction = null;
            }

            return true;
        }
        else if (actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            StartCoroutine(currentAction.Execute(this));
            return true;
        }

        return false;
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) RestartGame();

        if (ProcessActions()) return;

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
