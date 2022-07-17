using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Feature
{
    [SerializeField] Character characterPrefab = default;
    [SerializeField] Quaternion initialRotation = default;

    public override Character OnGameStarted(GameTile tile)
    {
        Character character = tile.SpawnCharacter(characterPrefab);
        character.Rotate(initialRotation * Vector3.forward);
        return character;
    }
}
