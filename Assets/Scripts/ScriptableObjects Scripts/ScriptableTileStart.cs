using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Start", menuName = "Tiles/Start", order = 2)]
public class ScriptableTileStart : ScriptableTileBase
{
    public void Awake()
    {
        prefabType = TileType.Start;
    }
}
