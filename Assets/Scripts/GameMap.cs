using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameMap : MonoBehaviour
{
    [field: SerializeField] public GameObject EditModeCursor { get; private set; }

    [SerializeField, HideInInspector] List<GameTile> tilemap = new List<GameTile>();

    public void PlaceTile(GameTile prefab, int x, int y)
    {
        RemoveTile(x, y);

        GameTile tile = (GameTile)PrefabUtility.InstantiatePrefab(prefab, transform);
        //GameTile tile = Instantiate(prefab, transform);
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

    public Character OnGameStarted()
    {
        Character player = null;

        foreach(GameTile tile in tilemap)
        {
            Character character = tile.OnGameStarted();
            if (character != null)
                player = character;
        }

        if (player == null) Debug.LogError("No spawner for player character placed in the map!");

        return player;
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
