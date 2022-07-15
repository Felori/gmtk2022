using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameTile Tile { get; private set; }

    public void SetTile(GameTile tile)
    {
        if (Tile != null) Tile.SetCharacter(null);

        Tile = tile;
        tile.SetCharacter(this);

        transform.position = tile.WorldPosition;
    }
}
