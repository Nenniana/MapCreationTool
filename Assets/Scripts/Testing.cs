using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour
{
    [SerializeField]
    private TilemapVisual tilemapVisual;

    [SerializeField]
    private TextMeshProUGUI tMPro;

    private Tilemap tilemap;
    private Sprite sprite;
    private string sOName;
    private Vector3 currentLocation = default;
    private Dictionary<int, Tilemap> tilemaps;
    private int brushSize = 1;
    private Vector3 cameraStart = new Vector3(100, 50, -10);

    [SerializeField]
    private int gridWidth = 10;
    [SerializeField]
    private int gridHeight = 10;
    [SerializeField]
    private float gridCellSizeX = 10f;
    [SerializeField]
    private float gridCellSizeY = 10f;
    [SerializeField]
    private int layerPadding = 10;

    //private GridBase<MapObject> gridBase;
    // Start is called before the first frame update
    void Start()
    {
        SetUpTilemap(gridWidth, gridHeight, gridCellSizeX, gridCellSizeY, currentLocation, GetNewLayer());
    }

    public Tilemap SetUpTilemap (int width, int height, float cellSizeX, float cellSizeY, Vector3 location, int layer)
    {
        if (tilemaps == null)
            tilemaps = new Dictionary<int, Tilemap>();

        tilemap = new Tilemap(width, height, cellSizeX, cellSizeY, location, layer);
        tilemaps.Add(layer, tilemap);

        tilemap.SetTilemapVisual(tilemapVisual);
        SetTilemapNewLocation(currentLocation);
        MoveCameraToActiveLayer();

        return tilemap;
    }

    public void SetTilemapNewLocation (Vector3 currentLocation)
    {
        this.currentLocation = currentLocation + new Vector3(0, (gridHeight * gridCellSizeY) + layerPadding);
    }

    public void SetSO (Sprite sprite, string sOName)
    {
        this.sprite = sprite;
        this.sOName = sOName;
        Debug.Log("Sprite chosen: " + sOName);
    }

    public void MoveCameraToActiveLayer ()
    {
        Camera.main.transform.position = cameraStart + tilemap.GetGridBase().GetOriginPosition();
    }

    public int GetNewLayer ()
    {
        if (tilemaps != null && tilemaps.Count > 0)
        {
            return tilemaps.Count + 1;
        }

        return 1;
    }

    public int GetNextLayer ()
    {
        int currentLayer = tilemap.GetCurrentLayer();

        if (tilemaps.Count == currentLayer)
        {
            return 1;
        }

        return currentLayer + 1;
    }

    public void SwitchTilemapLayer ()
    {
        if (tilemaps != null && tilemaps.Count > 1)
        {
            Debug.Log("Layer switched!");
            tilemap = tilemaps[GetNextLayer()];

            tilemap.SetTilemapVisual(tilemapVisual);
            MoveCameraToActiveLayer();
        }
    }

    public void SaveTilemaps ()
    {
        foreach (KeyValuePair<int, Tilemap> tilemap in tilemaps)
        {
            tilemap.Value.Save();
        }
    }

    [Serializable]
    public class SaveObject
    {
        public Tilemap.SaveObject[] tilemapArray;
    }

    public void Save ()
    {
        List<Tilemap.SaveObject> tilemapList = new List<Tilemap.SaveObject>();

        foreach (KeyValuePair<int, Tilemap>  tilemap in tilemaps)
        {
            tilemapList.Add(tilemap.Value.Save());
        }

        SaveObject saveObject = new SaveObject
        {
            tilemapArray = tilemapList.ToArray()
        };

        SaveSystem.SaveObject(saveObject);
    }

    public void Clear ()
    {
        if (tilemaps != null && tilemaps.Count > 1)
        {
            tilemapVisual.GetComponent<TilemapVisual>().Clear();

            foreach (KeyValuePair<int, Tilemap> tilemap in tilemaps)
            {
                tilemap.Value.Clear();
            }
        }
    }

    public void Load()
    {
        Clear();

        tilemaps = new Dictionary<int, Tilemap>();
        SaveObject saveObject = SaveSystem.LoadMostRecentObject<SaveObject>();

        foreach (Tilemap.SaveObject tilemapSaveObject in saveObject.tilemapArray)
        {
            Tilemap tilemap = SetUpTilemap(tilemapSaveObject.width, tilemapSaveObject.height, tilemapSaveObject.cellSizeX, tilemapSaveObject.cellSizeY, tilemapSaveObject.location, tilemapSaveObject.layer);
            tilemap.Load(tilemapSaveObject);
        }
    }

    private void DisplayCurrentTileObject ()
    {
        if (sOName != null)
            tMPro.text = "Selected: " + sOName;
        else
            tMPro.text = "Selected: none";
    }

    private void Update()
    {

        DisplayCurrentTileObject();

        if (Input.GetMouseButton(0) && tilemaps != null && tilemaps.Count >= 1)
        {
            Debug.Log("Painting");
            Vector3 position = GetMouseWorldPostion.GetMouseWorldPosition();
            tilemap.SetSOWithBrushSize(position, sprite, sOName, brushSize);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
            Debug.Log("Saved!");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //SceneManager.LoadScene(0);
            Load();
            Debug.Log("Loaded!");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            brushSize = 1;
            Debug.Log("Brushsize is now " + brushSize);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            brushSize = 2;
            Debug.Log("Brushsize is now " + brushSize);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            brushSize = 4;
            Debug.Log("Brushsize is now " + brushSize);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            brushSize = 9;
            Debug.Log("Brushsize is now " + brushSize);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            tilemap.SetAllSO(sprite, sOName);
            Debug.Log("Overwriting all!");
        }

        /*if (Input.GetKeyDown(KeyCode.N))
        {
            *//*tilemap.Save();
            Debug.Log("Saved!");*//*
            SetUpTilemap(gridWidth, gridHeight, gridCellSizeX, gridCellSizeY, currentLocation, GetNewLayer());
            Debug.Log("Adding new tilemap!");
        }*/

        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchTilemapLayer();
            Debug.Log("Attempting to switch layer!");
        }
    }
}
