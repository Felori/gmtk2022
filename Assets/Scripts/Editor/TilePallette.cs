using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile Pallette", menuName = "Map Editor/Tile Pallette")]
public class TilePallette : ScriptableObject
{
    [field: SerializeField] public GameTile[] Tiles { get; private set; }
    [field: SerializeField] public Feature[] Features { get; private set; }
}
