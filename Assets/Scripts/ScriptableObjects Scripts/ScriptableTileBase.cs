using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableTileBase : ScriptableObject
{
    [SerializeField]
    private string prefabName;
    [SerializeField]
    private Sprite prefabSprite;
    [SerializeField]
    private string description = "Fill me!";
    [SerializeField]
    private bool canBuild = false;
    [SerializeField]
    internal TileType prefabType;

    public TileType GetTileType ()
    {
        return prefabType;
    }

    public string GetPrefabName()
    {
        return prefabName;
    }

    public Sprite GetPrefabSprite()
    {
        return prefabSprite;
    }

    public string GetPrefabAbbr()
    {
        return description;
    }
}
