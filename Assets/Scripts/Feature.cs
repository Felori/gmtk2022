using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feature : MonoBehaviour
{
    public virtual Character OnGameStarted(GameTile tile)
    {
        return null;
    }
}
