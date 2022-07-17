using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public event Action onPlayerDied;

    protected override void Die()
    {
        Destroy(gameObject);
        onPlayerDied?.Invoke();
    }

    public void Attack(Enemy enemy, int damage)
    {
        enemy.TakeDamage(damage);
    }
}
