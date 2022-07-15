using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public (int x, int y) Position { get; private set; }

    public void SetTile(int x, int y)
    {
        Position = (x, y);
        transform.position = new Vector3(x, 0, y);
    }
}
