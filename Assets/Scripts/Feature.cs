using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Feature : MonoBehaviour
{
    public virtual Character OnGameStarted(GameTile tile)
    {
        return null;
    }
}
