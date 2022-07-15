using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameMap gameMap = default;
    [SerializeField] Character playerPrefab = default;
    [SerializeField] int mapSize = 10;

    Character player;

    void StartGame()
    {
        gameMap.GenerateMap(mapSize, mapSize);
        SpawnPlayer(0, 0);
    }

    void SpawnPlayer(int x, int y)
    {
        player = Instantiate(playerPrefab);
        player.SetTile(x, y);
    }

    void MovePlayer(int x, int y)
    {
        if (gameMap.IsValidPosition(x, y)) player.SetTile(x, y);
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        (int x, int y) = player.Position;

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
