using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Floor", menuName = "Tiles/Floor", order = 3)]
public class ScriptableTileEnd : ScriptableTileBase
{
    public void Awake()
    {
        prefabType = TileType.Floor;
    }
}
