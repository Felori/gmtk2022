using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour
{
    [SerializeField, HideInInspector] int x;
    [SerializeField, HideInInspector] int y;
    public (int x, int y) Position => (x, y);
    public Character Character { get; private set; }
    public Vector3 WorldPosition => transform.position;

    [SerializeField, HideInInspector] Feature feature;

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

        feature = Instantiate(featurePrefab, transform);
    }

    public void RemoveFeature()
    {
        if (feature) DestroyImmediate(feature.gameObject);
    }

    public void OnGameStarted()
    {
        feature?.OnGameStarted(this);
    }

    [ContextMenu("Print Position")]
    void PrintPosition()
    {
        Debug.Log(Position.ToString());
    }
}
