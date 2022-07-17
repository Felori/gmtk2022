using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class GameTile : MonoBehaviour
{
    [SerializeField, HideInInspector] int x;
    [SerializeField, HideInInspector] int y;
    public (int x, int y) Position => (x, y);
    public Character Character { get; private set; }
    public Vector3 WorldPosition => transform.position;

    [SerializeField, HideInInspector] Feature feature;

    Func<int, int, GameTile> tileProvider;

    public void Setup(int x, int y)
    {
        this.x = x;
        this.y = y;

        gameObject.name = "Tile (" + Position.x + ", " + Position.y + ")";
        transform.localPosition = new Vector3(x, 0, y);
    }

    public void SetCharacter(Character character)
    {
        Character = character;
    }

    public void PlaceFeature(Feature featurePrefab)
    {
        RemoveFeature();

        feature = (Feature)PrefabUtility.InstantiatePrefab(featurePrefab, transform);

        EditorUtility.SetDirty(this);
    }

    public void RemoveFeature()
    {
        if (feature) DestroyImmediate(feature.gameObject);

        EditorUtility.SetDirty(this);
    }

    public void Rotate()
    {
        transform.Rotate(Vector3.up, 90f);
    }

    public GameTile[] GetNeighbors()
    {
        List<GameTile> neighbors = new List<GameTile>();

        neighbors.Add(tileProvider(x + 1, y));
        neighbors.Add(tileProvider(x - 1, y));
        neighbors.Add(tileProvider(x, y + 1));
        neighbors.Add(tileProvider(x, y - 1));

        neighbors.RemoveAll(tile => tile == null);

        return neighbors.ToArray();
    }

    public Character OnGameStarted(Func<int, int, GameTile> tileProvider)
    {
        this.tileProvider = tileProvider;

        if (feature == null) return null;

        return feature.OnGameStarted(this);
    }

    public void ResetTile()
    {
        if(Character != null)
        {
            Destroy(Character.gameObject);
            Character = null;
        }
    }

    [ContextMenu("Print Position")]
    void PrintPosition()
    {
        Debug.Log(Position.ToString());
    }
}
