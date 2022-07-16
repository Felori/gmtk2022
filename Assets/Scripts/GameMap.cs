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

    public Character[] OnGameStarted()
    {
        List<Character> characters = new List<Character>();

        foreach(GameTile tile in tilemap)
        {
            characters.Add(tile.OnGameStarted(GetTile));
        }

        characters.RemoveAll(character => character == null);

        return characters.ToArray();
    }

    public GameTile GetTile(int x, int y)
    {
        foreach(GameTile tile in tilemap)
        {
            if (tile.Position == (x, y)) return tile;
        }

        return null;
    }

    [ContextMenu("Print Tilemap Size")]
    void PrintTilemapSize()
    {
        Debug.Log(tilemap.Count);
    }

    [ContextMenu("Remove null tiles from tilemap")]
    void RemoveNullTiles()
    {
        tilemap.RemoveAll(item => item == null);

        EditorUtility.SetDirty(this);
    }
}
