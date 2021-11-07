using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default", menuName = "Tiles/Default", order = 2)]
public class ScriptableTileDefault : ScriptableTileBase
{
    public void Awake()
    {
        prefabType = TileType.Default;
    }
}
