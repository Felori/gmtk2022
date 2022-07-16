using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    [field: SerializeField] public GameObject EditModeCursor { get; private set; }

    List<GameTile> tilemap = new List<GameTile>();

    public void PlaceTile(GameTile prefab, int x, int y)
    {
        RemoveTile(x, y);

        GameTile tile = Instantiate(prefab, new Vector3(x, 0, y), Quaternion.identity, transform);
        tile.Setup(x, y);
        tilemap.Add(tile);
    }

    public void RemoveTile(int x, int y)
    {
        GameTile tile = GetTile(x, y);

        if (tile == null) return;

        DestroyImmediate(tile.gameObject);
        tilemap.Remove(tile);
    }

    public GameTile GetTile(int x, int y)
    {
        foreach(GameTile tile in tilemap)
        {
            if (tile.Position == (x, y)) return tile;
        }

        return null;
    }
}
