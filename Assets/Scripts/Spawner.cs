using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Feature
{
    [SerializeField] Character characterPrefab = default;
    [SerializeField] Quaternion initialRotation = default;

    public override Character OnGameStarted(GameTile tile)
    {
        Character character = Instantiate(characterPrefab);
        character.SetTile(tile);
        character.transform.position = tile.transform.position;
        character.Rotate(initialRotation * Vector3.forward);
        return character;
    }
}
