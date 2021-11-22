using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap
{
    public event EventHandler OnLoaded;
    private GridBase<TilemapObject> gridBase;
    private TilemapObject currenttilemapObject;

    public Tilemap (int width, int height, float cellSizeX, float CellSizeY, Vector3 originPosition = default, int layer = 1)
    {
        gridBase = new GridBase<TilemapObject>(width, height, cellSizeX, CellSizeY, (GridBase<TilemapObject> g, int x, int y) => new TilemapObject(g, x, y), originPosition, layer);
    }

    public void SetSOWithBrushSize(Vector3 worldPosition, Sprite sprite, string sOName, int brushSize)
    {
        foreach (Vector3 location in BrushSystem.BrushSize(worldPosition, brushSize, gridBase.GetCellSizeX(), gridBase.GetCellSizeY()))
        {
            currenttilemapObject = gridBase.GetGridObject(location);

            if (currenttilemapObject != null)
            {
                currenttilemapObject.SetSO(sprite, sOName);
            }
        }
    }

    public void SetAllSO(Sprite sprite, string sOName)
    {
        for (int x = 0; x < gridBase.GetWidth(); x++)
        {
            for (int y = 0; y < gridBase.GetHeight(); y++)
            {
                TilemapObject tilemapObject = gridBase.GetGridObject(x, y);
                tilemapObject.SetSO(sprite, sOName);
            }
        }
    }

    public int GetCurrentLayer ()
    {
        return gridBase.GetLayer();
    }

    public void UpdateTilemapVisual (TilemapVisual tilemapVisual)
    {
        tilemapVisual.ChangeGrid(this, gridBase);
    }

    public void SetTilemapVisual (TilemapVisual tilemapVisual)
    {
        tilemapVisual.SetGridBase(this, gridBase);
    }

    public GridBase<TilemapObject> GetGridBase ()
    {
        return gridBase;
    }

    [Serializable]
    public class SaveObject
    {
        public int height;
        public int width;
        public float cellSizeX;
        public float cellSizeY;
        public Vector3 location;
        public int layer;
        public TilemapObject.SaveObject[] tilemapObjectSaveObjectArray;
    }

    public SaveObject SaveForScriptable ()
    {
        List<TilemapObject.SaveObject> tilemapObjectSaveObjectList = new List<TilemapObject.SaveObject>();

        for (int x = 0; x < gridBase.GetWidth(); x++)
        {
            for (int y = 0; y < gridBase.GetHeight(); y++)
            {
                TilemapObject tilemapObject = gridBase.GetGridObject(x, y);
                tilemapObjectSaveObjectList.Add(tilemapObject.Save());
            }
        }

        return new SaveObject
        {
            tilemapObjectSaveObjectArray = tilemapObjectSaveObjectList.ToArray(),
            layer = gridBase.GetLayer(),
            height = gridBase.GetHeight(),
            width = gridBase.GetWidth(),
            cellSizeX = gridBase.GetCellSizeX(),
            cellSizeY = gridBase.GetCellSizeY(),
            location = gridBase.GetOriginPosition()
        };
    }

    public void LoadScriptable(ScriptableRoomTemplate.RoomLayer saveObject)
    {
        foreach (var tilemapObjectSaveObject in saveObject.roomTiles)
        {
            if (tilemapObjectSaveObject.sOName != "")
            {
                TilemapObject tilemapObject = gridBase.GetGridObject(tilemapObjectSaveObject.x, tilemapObjectSaveObject.y);
                tilemapObject.LoadScriptable(tilemapObjectSaveObject);
                gridBase.TriggerGridBaseObjectChanged(tilemapObjectSaveObject.x, tilemapObjectSaveObject.y);
            }
        }

        OnLoaded?.Invoke(this, EventArgs.Empty);
    }
}
