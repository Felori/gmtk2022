using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Timings
{
    // How long does one step of the character take
    public const float PLAYER_MOVE_DURATION = 0.2f;

    // How long from start of the attack until the target takes damage
    public const float HIT_DELAY = 0.15f;

    // How long does an entire attack take
    public const float ATTACK_DURATION = 1f;

    // Delay until enemies take turns
    public const float ENEMY_ACTION_DELAY = 0.3f;

    // Length of the time between player turns (excluding the time it takes to process enemy actions)
    public const float NEW_TURN_DELAY = 0.6f;
}
