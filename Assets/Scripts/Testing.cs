using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEditor;

public class Testing : MonoBehaviour
{
    [SerializeField]
    private TilemapVisual tilemapVisual;

    [SerializeField]
    private TextMeshProUGUI tMPro;

    [SerializeField]
    private TMP_Dropdown dropdown;

    [SerializeField]
    private TMP_InputField text;

    private Tilemap tilemap;
    private Sprite sprite;
    private string sOName;
    private Vector3 currentLocation = default;
    private Dictionary<int, Tilemap> tilemaps;
    private int brushSize = 1;
    private Vector3 cameraStart = new Vector3(100, 50, -10);

    private int dropdownValue;

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
        brushSize = 1;
        //SetUpTilemap(gridWidth, gridHeight, gridCellSizeX, gridCellSizeY, currentLocation, GetNewLayer());
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene arg0, LoadSceneMode mode)
    {
        SetLoadDropDown();

        if (mode == LoadSceneMode.Single)
        {
            Debug.Log("Scene reset.");
            
            LoadScriptable(LoadFileName.LoadName);
        }
            
        else
            SetUpTilemap(gridWidth, gridHeight, gridCellSizeX, gridCellSizeY, currentLocation, GetNewLayer());
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
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

            tilemap.UpdateTilemapVisual(tilemapVisual);
            MoveCameraToActiveLayer();
        }
    }


    public void SaveScriptable()
    {
        ScriptableRoomTemplate scriptableRoomTemplate = ScriptableObject.CreateInstance("ScriptableRoomTemplate") as ScriptableRoomTemplate;

        scriptableRoomTemplate.init(text.text, gridHeight, gridWidth, gridCellSizeX, gridCellSizeY);

        foreach (KeyValuePair<int, Tilemap> tilemap in tilemaps)
        {
            scriptableRoomTemplate.AddLayer(tilemap.Value.SaveForScriptable());
        }

        AssetDatabase.CreateAsset(scriptableRoomTemplate, "Assets/Resources/Saves/" + text.text + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void LoadScriptable(string saveName)
    {
        Debug.Log("Loaded!");

        ScriptableRoomTemplate scriptableRoomTemplate = SODatabase.GetSaveByName(saveName);

        foreach (var tilemapSaveObject in scriptableRoomTemplate.roomLayers)
        {
            Tilemap tilemap = SetUpTilemap(scriptableRoomTemplate.roomWidth, scriptableRoomTemplate.roomHeight, scriptableRoomTemplate.roomCellSizeX, scriptableRoomTemplate.roomCellSizeY, tilemapSaveObject.location, tilemapSaveObject.layer);
            tilemap.LoadScriptable(tilemapSaveObject);
        }
    }

    private void DisplayCurrentTileObject ()
    {
        if (sOName != null)
            tMPro.text = "Selected: " + sOName;
        else
            tMPro.text = "Selected: none";
    }

    private void SetLoadDropDown()
    {
        dropdown.AddOptions(SODatabase.GetAllSaveNames());
    }

    private void Update()
    {

        bool IsEditingInputField = EventSystem.current.currentSelectedGameObject?.TryGetComponent(out TMP_InputField _) ?? false;

        DisplayCurrentTileObject();
        if (!IsEditingInputField) 
        {
            if (Input.GetMouseButton(0) && tilemaps != null && tilemaps.Count >= 1)
            {
                Debug.Log("Painting");
                Vector3 position = GetMouseWorldPostion.GetMouseWorldPosition();
                tilemap.SetSOWithBrushSize(position, sprite, sOName, brushSize);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                //Save();
                SaveScriptable();
                Debug.Log("Saved!");
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("dropdown.options[dropdownValue].text " + dropdown.options[dropdown.value].text);
                LoadFileName.LoadName = dropdown.options[dropdown.value].text;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

            if (Input.GetKeyDown(KeyCode.N))
            {
                /*tilemap.Save();
                Debug.Log("Saved!");*/
                SetUpTilemap(gridWidth, gridHeight, gridCellSizeX, gridCellSizeY, currentLocation, GetNewLayer());
                Debug.Log("Adding new tilemap!");
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                SwitchTilemapLayer();
                Debug.Log("Attempting to switch layer!");
            }
        }
    }
}
