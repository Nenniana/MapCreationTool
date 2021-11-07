using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TilemapVisual : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;

    private Sprite sprite;
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

    public void SetGridBase (Tilemap tilemap, GridBase<TilemapObject> gridBase)
    {
        this.gridBase = gridBase;
        CreateTilemapArray();
        UpdateTilemapVisual();

        gridBase.OnGridValueChanged += Grid_OnGridValueChanged;
        tilemap.OnLoaded += Tilemap_OnLoaded;
    }

    private void Tilemap_OnLoaded(object sender, EventArgs e)
    {
        UpdateTilemapVisual();
    }

    private void Grid_OnGridValueChanged(object sender, GridBase<TilemapObject>.OnGridValueChangedEventArgs e)
    {
        UpdateTilemapVisual();
    }

    public void Clear ()
    {
        gridBase.OnGridValueChanged -= Grid_OnGridValueChanged;

        foreach (GameObject tile in tiles)
        {
            GameObject.Destroy(tile);
        }

        gridBase = null;
        tiles = null;
        layers = null;
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
                currentTile.GetComponent<TileVisualObject>().SetLocation(x, y);
                currentTile.GetComponent<SpriteRenderer>().sprite = sprite;
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

    private void UpdateTilemapVisual ()
    {
        Debug.Log("UpdateTilemapVisual was run!");

        foreach (GameObject tile in tiles)
        {
            TilemapObject tilemapObject = gridBase.GetGridObject(tile.transform.localPosition - new Vector3(gridBase.GetCellSizeX(), gridBase.GetCellSizeY()) * 0.5f);
            tile.GetComponent<SpriteRenderer>().sprite = tilemapObject.GetSprite();
        }
    }
}
