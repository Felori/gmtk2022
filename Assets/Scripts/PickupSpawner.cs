using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : Feature
{
    [SerializeField] Enemy enemyPrefab = default;
    [SerializeField] Pickup[] pickups = default;
    [SerializeField] GameObject model = default;

    bool used = false;
    public override bool Interactable => !used;

    public static event Action<Character> onEnemySpawned;

    public override bool Interact(Player player, int actionPoints, GameTile tile, GameManager manager)
    {
        int result = UnityEngine.Random.Range(0, pickups.Length + 1);

        if (result == pickups.Length)
        {
            Character enemy = tile.SpawnCharacter(enemyPrefab);
            enemy.LookAt(player.transform.position);
            onEnemySpawned?.Invoke(enemy);
        }
        else
        {
            Pickup pickup = Instantiate(pickups[result], tile.transform.position + Vector3.up, Quaternion.identity);
            pickup.Apply(manager);
        }

        used = true;
        model.SetActive(false);

        return true;
    }
}
