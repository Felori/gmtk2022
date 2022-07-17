using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] int damage = 1;
    [SerializeField] Animator animator = default;

    public event Action onDied;

    public void DoEnemyTurn()
    {
        GameTile[] neighborTiles = Tile.GetNeighbors();

        foreach(GameTile tile in neighborTiles)
        {
            if (tile.Character != null && tile.Character is Player player) Attack(player);
        }
    }

    void Attack(Player player)
    {
        LookAt(player.transform.position);
        player.TakeDamage(damage);
        animator.SetTrigger("Attack");
    }

    protected override void Die()
    {
        onDied?.Invoke();
        Destroy(gameObject);
    }
}
