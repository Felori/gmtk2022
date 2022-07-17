using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Character
{
    [SerializeField] TMP_Text actionPointsText = default;

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

    public void SetRemainingActionPoints(int points)
    {
        actionPointsText.text = points.ToString();
    }
}
