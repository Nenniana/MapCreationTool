using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wall", menuName = "Tiles/Wall", order = 2)]
public class ScriptableTilePath : ScriptableTileBase
{
    public void Awake()
    {
        prefabType = TileType.Wall;
    }
}
