using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Floor", menuName = "Tiles/Floor", order = 2)]
public class ScriptableTileDefault : ScriptableTileBase
{
    public void Awake()
    {
        prefabType = TileType.Floor;
    }
}
