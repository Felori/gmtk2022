using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Feature : MonoBehaviour
{
    public virtual bool Interactable => false;

    public virtual Character OnGameStarted(GameTile tile)
    {
        return null;
    }

    public virtual bool Interact(Player player, int actionPoints, GameTile tile, GameManager manager)
    {
        return false;
    }
}
