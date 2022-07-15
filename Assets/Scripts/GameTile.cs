using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour
{
    public (int x, int y) Position { get; private set; }
    public Character Character { get; private set; }
    public Vector3 WorldPosition => transform.position;

    public void Setup(int x, int y)
    {
        Position = (x, y);

        gameObject.name = "Tile (" + Position.x + ", " + Position.y + ")";
        transform.localPosition = new Vector3(x, 0, y);
    }

    public void SetCharacter(Character character)
    {
        Character = character;
    }

    public override string ToString()
    {
        return gameObject.name;
    }
}
