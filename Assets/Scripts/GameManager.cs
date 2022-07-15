using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameMap gameMap = default;
    [SerializeField] Character playerPrefab = default;
    [SerializeField] int mapSize = 10;

    void StartGame()
    {
        gameMap.GenerateMap(mapSize, mapSize);
        SpawnPlayer(0, 0);
    }

    void SpawnPlayer(int x, int y)
    {
        Character player = Instantiate(playerPrefab);
        GameTile spawnTile = gameMap.GetTile(x, y);
        player.transform.position = spawnTile.Position;
    }

    private void Start()
    {
        StartGame();
    }
}
