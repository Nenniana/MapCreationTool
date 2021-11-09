using System;
using UnityEngine;

public class GridBase<TGridBaseObject>
{

    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;
    private int height;
    private float cellSizeX;
    private float cellSizeY;
    private Vector3 originPosition;
    private int layer;
    private TGridBaseObject[,] gridArray;
    private bool showDebug = true;
    private TextMesh[,] debugTextArray;

    public GridBase (int width, int height, float cellSizeX, float cellSizeY, Func<GridBase<TGridBaseObject>, int, int, TGridBaseObject> createGridBaseObject, Vector3 originPosition = default, int layer = 1)
    {
        this.width = width;
        this.height = height;
        this.cellSizeX = cellSizeX;
        this.cellSizeY = cellSizeY;
        this.originPosition = originPosition;
        this.layer = layer;

        gridArray = new TGridBaseObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridBaseObject(this, x, y);
            }
        }

        if (showDebug)
        {
            debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    debugTextArray[x, y] = DisplayWorldText.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSizeX, cellSizeY) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y) + new Vector3(0, cellSizeY), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y) + new Vector3(cellSizeX, 0), Color.white, 100f);
                }
            }

            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

            OnGridValueChanged += Grid_OnGridValueChanged;
        }
    }

    private void Grid_OnGridValueChanged(object sender, OnGridValueChangedEventArgs e)
    {
        debugTextArray[e.x, e.y].text = gridArray[e.x, e.y]?.ToString();
    }

    public int GetLayer()
    {
        return layer;
    }

    public int GetHeight ()
    {
        return height;
    }

    public int GetWidth ()
    {
        return width;
    }

    public float GetCellSizeX ()
    {
        return cellSizeX;
    }

    public float GetCellSizeY()
    {
        return cellSizeY;
    }

    public Vector3 GetOriginPosition ()
    {
        return originPosition;
    }

    public void TriggerGridBaseObjectChanged (int x, int y)
    {
        if (OnGridValueChanged != null)
        {
            OnGridValueChanged(this, new OnGridValueChangedEventArgs
            {
                x = x,
                y = y
            });
        }
    }

    public void SetGridObject (int x, int y, TGridBaseObject value)
    { 
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            if (OnGridValueChanged != null)
            {
                OnGridValueChanged(this, new OnGridValueChangedEventArgs
                {
                    x = x,
                    y = y
                });
            }
        } 
    }

    public void Clear()
    {
        foreach (var item in debugTextArray)
        {
            OnGridValueChanged -= Grid_OnGridValueChanged;
            GameObject.Destroy(item);
        }

        gridArray = null;
        showDebug = false;
    }

    public void SetGridObject (Vector3 worldPosition, TGridBaseObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    public TGridBaseObject GetGridObject (int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        } 
        else
        {
            return default;
        }
    }

    public TGridBaseObject GetGridObject (Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }

    private void GetXY (Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSizeX);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSizeY);
    }

    public Vector3 GetWorldPosition (int x, int y)
    {
        return new Vector3(x * cellSizeX, y * cellSizeY) + originPosition;
    }
}
