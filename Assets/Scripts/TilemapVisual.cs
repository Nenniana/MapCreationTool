using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TilemapVisual : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;

    private int currentLayer;
    private SortedDictionary<int, List<GameObject>> layers;
    private List<GameObject> tiles;
    private GridBase<TilemapObject> gridBase;

    private int GetNextLayer ()
    {
        if (layers != null && layers.Count != 0)
            return layers.Keys.Max();
        else
            return 0;
    }

    public void ChangeGrid (Tilemap tilemap, GridBase<TilemapObject> gridBase)
    {
        this.gridBase = gridBase;
        currentLayer = tilemap.GetCurrentLayer();
    }

    public void SetGridBase (Tilemap tilemap, GridBase<TilemapObject> gridBase)
    {
        currentLayer = tilemap.GetCurrentLayer();
        this.gridBase = gridBase;
        CreateTilemapArray();
        UpdateTilemapVisual(currentLayer);

        gridBase.OnGridValueChanged += Grid_OnGridValueChanged;
        tilemap.OnLoaded += Tilemap_OnLoaded;
    }

    private void Tilemap_OnLoaded(object sender, EventArgs e)
    {
        UpdateTilemapVisual(currentLayer);
    }

    private void Grid_OnGridValueChanged(object sender, GridBase<TilemapObject>.OnGridValueChangedEventArgs e)
    {
        UpdateTilemapVisual(currentLayer);
    }

    private void CreateTilemapArray()
    {
        AddNewTilemapLayer();

        for (int x = 0; x < gridBase.GetWidth(); x++)
        {
            for (int y = 0; y < gridBase.GetHeight(); y++)
            {
                GameObject currentTile = GameObject.Instantiate(tile);
                Transform currentTransform = currentTile.transform;
                currentTransform.localPosition = gridBase.GetWorldPosition(x, y) + new Vector3(gridBase.GetCellSizeX(), gridBase.GetCellSizeY()) * 0.5f;
                currentTransform.localScale = new Vector3(gridBase.GetCellSizeX(), gridBase.GetCellSizeY());
                tiles.Add(currentTile);
            }
        }
    }

    private void AddNewTilemapLayer()
    {
        tiles = new List<GameObject>();

        if (layers == null)
            layers = new SortedDictionary<int, List<GameObject>>();

        layers.Add(GetNextLayer() + 1, tiles);
    }

    private void UpdateTilemapVisual(int layer)
    {
        Debug.Log("UpdateTilemapVisual was run!");

        foreach (GameObject tile in layers[layer])
        {
            TilemapObject tilemapObject = gridBase.GetGridObject(tile.transform.localPosition - new Vector3(gridBase.GetCellSizeX(), gridBase.GetCellSizeY()) * 0.5f);
            tile.GetComponent<SpriteRenderer>().sprite = tilemapObject.GetSprite();
        }
    }
}
