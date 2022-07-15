using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    [SerializeField] GameTile tilePrefab = default;

    int width;
    int height;

    GameTile[,] tilemap;

    public void GenerateMap(int width, int height)
    {
        this.width = width;
        this.height = height;

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
        tile.Setup(x, y);
        tilemap[x, y] = tile;
    }

    public bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    public GameTile GetTile(int x, int y)
    {
        if (!IsValidPosition(x, y)) return null;

        return tilemap[x, y];
    }
}
