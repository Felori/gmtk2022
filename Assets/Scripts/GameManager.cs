using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameMap gameMap = default;
    [SerializeField] Character playerPrefab = default;
    [SerializeField] Character enemyPrefab = default;
    [SerializeField] int mapSize = 10;

    Character player;

    void StartGame()
    {
        //gameMap.GenerateMap(mapSize, mapSize);
        /*player = SpawnCharacter(playerPrefab, 0, 0);
        SpawnCharacter(enemyPrefab, mapSize / 2, mapSize / 2);
        SpawnCharacter(enemyPrefab, mapSize - 1, mapSize - 1);*/
    }

    Character SpawnCharacter(Character prefab, int x, int y)
    {
        Character character = Instantiate(prefab);
        GameTile tile = gameMap.GetTile(x, y);
        character.SetTile(tile);
        return character;
    }

    void MovePlayer(int x, int y)
    {
        GameTile tile = gameMap.GetTile(x, y);
        if (tile != null && tile.Character == null)
        {
            player.SetTile(tile);
        }
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
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
