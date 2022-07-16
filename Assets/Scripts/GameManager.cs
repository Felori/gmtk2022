using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameMap gameMap = default;

    Character player;
    int playerActionPoints = 0;

    void StartGame()
    {
        player = gameMap.OnGameStarted();
        RollMovement();
    }

    void EndTurn()
    {
        Debug.Log("End Turn");
        RollMovement();
    }

    void MovePlayer(int x, int y)
    {
        if (playerActionPoints == 0) return;

        GameTile tile = gameMap.GetTile(x, y);
        if (tile != null && tile.Character == null)
        {
            player.SetTile(tile);
            playerActionPoints--;
            if (playerActionPoints == 0) EndTurn();
        }
    }

    void RollMovement()
    {
        playerActionPoints = Random.Range(1, 7);
        Debug.Log("Rolled " + playerActionPoints);
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
