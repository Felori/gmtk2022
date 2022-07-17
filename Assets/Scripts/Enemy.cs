using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : Character
{
    [SerializeField] int damage = 1;
    [SerializeField] TMP_Text healthText = default;

    public event Action onDied;

    public GameAction DoEnemyTurn()
    {
        GameTile[] neighborTiles = Tile.GetNeighbors();

        foreach(GameTile tile in neighborTiles)
        {
            if (tile.Character != null && tile.Character is Player player) return new CharacterAttackAction(this, player, damage);
        }

        return null;
    }

    protected override void SetHealth(int health)
    {
        base.SetHealth(health);
        health = Mathf.Max(health, 0);
        healthText.text = health.ToString();
    }

    protected override void Die()
    {
        base.Die();
        onDied?.Invoke();
        healthText.gameObject.SetActive(false);
    }
}
