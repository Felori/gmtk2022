using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameTile Tile { get; private set; }

    [SerializeField] protected Animator animator;

    [SerializeField] int maxHealth = 5;
    [SerializeField] Transform model = default;

    protected int health;

    public void SetTile(GameTile tile)
    {
        if (Tile != null) Tile.SetCharacter(null);

        Tile = tile;
        tile.SetCharacter(this);
    }

    public void LookAt(Vector3 point)
    {
        Vector3 dir = point - transform.position;
        Rotate(dir);
    }

    public void Rotate(Vector3 dir)
    {
        Vector2 dir2 = new Vector2(dir.x, dir.z);
        float angle = Vector2.SignedAngle(dir2, Vector2.up);
        model.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void TakeDamage(int damage)
    {
        ChangeHealth(-damage);
        if (health <= 0) Die();
        else animator.SetTrigger("Take Damage");
    }

    public void ChangeHealth(int delta)
    {
        SetHealth(health + delta);
    }

    protected virtual void SetHealth(int health)
    {
        this.health = health;
    }

    protected virtual void Die()
    {
        Tile.SetCharacter(null);
        animator.SetTrigger("Die");
        Destroy(gameObject, 3f);
    }

    private void Start()
    {
        SetHealth(maxHealth);
    }
}
