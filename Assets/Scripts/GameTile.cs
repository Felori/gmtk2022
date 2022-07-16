using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class GameTile : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y;
    public (int x, int y) Position => (x, y);
    public Character Character { get; private set; }
    public Vector3 WorldPosition => transform.position;

    public Feature feature;

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

    public Character OnGameStarted()
    {
        if (feature == null) return null;

        return feature.OnGameStarted(this);
    }

    [ContextMenu("Print Position")]
    void PrintPosition()
    {
        Debug.Log(Position.ToString());
    }
}
