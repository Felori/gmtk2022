using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction
{
    public bool Done { get; protected set; } = false;

    public abstract IEnumerator Execute(GameManager manager);
}

public class PlayerMoveAction : GameAction
{
    Player player;

    Vector3 from;
    Vector3 to;

    float duration = Timings.PLAYER_MOVE_DURATION;

    public PlayerMoveAction(Player player, Vector3 from, Vector3 to)
    {
        this.player = player;
        this.from = from;
        this.to = to;
    }

    public override IEnumerator Execute(GameManager manager)
    {
        float progress = 0f;

        player.Rotate(to - from);

        player.OnPlayerMoved();

        while (progress < 1f)
        {
            progress += Time.deltaTime / duration;
            player.transform.position = Vector3.Lerp(from, to, Mathf.Clamp01(progress));
            yield return new WaitForEndOfFrame();
        }

        Done = true;

        yield return null;
    }
}

public class CharacterAttackAction : GameAction
{
    Character attacker;
    Character target;

    int damage;

    public CharacterAttackAction(Character attacker, Character target, int damage)
    {
        this.attacker = attacker;
        this.target = target;
        this.damage = damage;
    }

    public override IEnumerator Execute(GameManager manager)
    {
        attacker.LookAt(target.transform.position);
        target.LookAt(attacker.transform.position);

        attacker.Attack();

        yield return new WaitForSeconds(Timings.HIT_DELAY);

        target.TakeDamage(damage);

        yield return new WaitForSeconds(Timings.ATTACK_DURATION - Timings.HIT_DELAY);

        Done = true;
    }
}

public class ControlAction : GameAction
{
    Action action;

    public ControlAction(Action action)
    {
        this.action = action;
    }

    public override IEnumerator Execute(GameManager manager)
    {
        action();
        Done = true;
        yield return null;
    }
}

public class EndTurnAction : GameAction
{
    IEnumerable<Enemy> enemies;

    public EndTurnAction(IEnumerable<Enemy> enemies)
    {
        this.enemies = enemies;
    }

    public override IEnumerator Execute(GameManager manager)
    {
        IEnumerable<GameAction> enemyActions = enemies.Select(enemy => enemy.DoEnemyTurn()).Where(action => action != null);

        yield return new WaitForSeconds(Timings.ENEMY_ACTION_DELAY);

        foreach(GameAction action in enemyActions)
        {
            yield return manager.StartCoroutine(action.Execute(manager));
        }

        yield return new WaitForSeconds(Timings.NEW_TURN_DELAY - Timings.ENEMY_ACTION_DELAY);

        Done = true;
    }
}