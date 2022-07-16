using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Feature
{
    [SerializeField] Character characterPrefab = default;

    public override void OnGameStarted(GameTile tile)
    {
        Character character = Instantiate(characterPrefab);
        character.SetTile(tile);
    }
}
