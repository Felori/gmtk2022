using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Character
{
    [SerializeField] TMP_Text actionPointsText = default;

    public event Action onPlayerDied;
    public event Action<int> onHealthChanged;

    protected override void Die()
    {
        base.Die();
        actionPointsText.gameObject.SetActive(false);
        onPlayerDied?.Invoke();
    }

    protected override void OnDamageTaken()
    {
        if(health > 0)
            animator.SetTrigger("Take Damage");
    }

    protected override void SetHealth(int health)
    {
        base.SetHealth(health);
        onHealthChanged?.Invoke(health);
    }

    public void Attack(Enemy enemy, int damage)
    {
        animator.SetTrigger("Attack");
        enemy.TakeDamage(damage);
    }

    public void SetRemainingActionPoints(int points)
    {
        actionPointsText.text = points.ToString();
    }

    public void OnPlayerMoved()
    {
        animator.Play("Player Move", 0, 0);

        //animator.SetTrigger("Move");
	}
}
