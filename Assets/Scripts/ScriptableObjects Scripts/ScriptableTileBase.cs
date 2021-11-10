using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableTileBase : ScriptableObject
{
    [SerializeField]
    private string prefabName;
    [SerializeField]
    private GameObject prefabObject;
    [SerializeField]
    private Sprite prefabSprite;
    [SerializeField]
    private string description = "Fill me!";
    [SerializeField]
    internal TileType prefabType;

    private void OnEnable()
    {
        prefabName = this.name;
    }

    public TileType GetTileType ()
    {
        return prefabType;
    }

    public string GetPrefabName()
    {
        return prefabName;
    }

    public GameObject GetPrefabObject()
    {
        return prefabObject;
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
