using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Feature
{
    [SerializeField] Character characterPrefab = default;
    [SerializeField] bool isPlayerSpawner = false;

    public override Character OnGameStarted(GameTile tile)
    {
        Character character = Instantiate(characterPrefab);
        character.SetTile(tile);

        if (isPlayerSpawner) return character;
        return null;
    }
}
