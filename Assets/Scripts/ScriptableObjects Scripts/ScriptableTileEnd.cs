using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "End", menuName = "Tiles/End", order = 3)]
public class ScriptableTileEnd : ScriptableTileBase
{
    public void Awake()
    {
        prefabType = TileType.End;
    }
}
