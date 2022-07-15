using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    [SerializeField] GameTile tilePrefab = default;

    GameTile[,] tilemap;

    public void GenerateMap(int width, int height)
    {
        tilemap = new GameTile[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                CreateTile(x, y);
            }
        }
    }

    void CreateTile(int x, int y)
    {
        GameTile tile = Instantiate(tilePrefab, transform);
        tile.transform.localPosition = new Vector3(x, 0, y);
        tilemap[x, y] = tile;
    }

    public GameTile GetTile(int x, int y)
    {
        return tilemap[x, y];
    }

    private void Start()
    {
        GenerateMap(10, 10);
    }
}
