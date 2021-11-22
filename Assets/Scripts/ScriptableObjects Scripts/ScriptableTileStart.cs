using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interactable", menuName = "Tiles/Interactable", order = 2)]
public class ScriptableTileStart : ScriptableTileBase
{
    public void Awake()
    {
        prefabType = TileType.Interactable;
    }
}
