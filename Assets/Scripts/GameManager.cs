using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameMap gameMap = default;
    [SerializeField] ActionPointsUI actionPointsUi = default;
    [SerializeField] CinemachineVirtualCamera playerCam = default;

    Player player;
    List<Enemy> enemies = new List<Enemy>();

    int[] actionPoints = new int[3];

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
        Character[] characters = gameMap.OnGameStarted();
        foreach(Character character in characters)
        {
            if (character is Player player) this.player = player;
            if (character is Enemy enemy) enemies.Add(enemy);
        }

        if (player == null)
        {
            Debug.LogError("No player found in the map!");
            return;
        }

        playerCam.Follow = player.transform;

        actionPoints[0] = RollDice();
        actionPoints[1] = RollDice();
        actionPoints[2] = RollDice();

        actionPointsUi.SetActionPoints(actionPoints);
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

        actionPointsUi.SetActionPoints(actionPoints);
    }

    void MovePlayer(int x, int y)
    {
        if (CurrentActionPoints == 0) return;

        GameTile tile = gameMap.GetTile(x, y);
        if (tile != null && tile.Character == null)
        {
            player.LookAt(tile.transform.position);
            player.SetTile(tile);
            CurrentActionPoints--;
            if (CurrentActionPoints == 0) EndTurn();
        }
    }

    int RollDice()
    {
        return Random.Range(1, 7);
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (player == null) return;

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
}
