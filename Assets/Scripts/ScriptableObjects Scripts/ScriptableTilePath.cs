using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Path", menuName = "Tiles/Path", order = 2)]
public class ScriptableTilePath : ScriptableTileBase
{
    public void Awake()
    {
        prefabType = TileType.Path;
    }
}
