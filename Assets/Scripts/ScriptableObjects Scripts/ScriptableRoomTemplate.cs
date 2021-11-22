using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScriptableRoomTemplate : ScriptableObject
{
    public string roomName;
    public RoomType roomType = RoomType.Normal;
    public string roomDescription = "Fill me!";
    public List<RoomLayer> roomLayers;
    public int roomHeight;
    public int roomWidth;
    public float roomCellSizeX;
    public float roomCellSizeY;

    [Serializable]
    public class RoomLayer
    {
        public int layer;
        public Vector2 location;
        public List<RoomTile> roomTiles = new List<RoomTile>();

        public RoomLayer(int layer, Vector2 location)
        {
            this.layer = layer;
            this.location = location;
        }
    }

    [Serializable]
    public class RoomTile
    {
        public Sprite sprite;
        public string sOName;
        public int x;
        public int y;
        [SerializeField]
        Dialogue[] dialogues;
        public bool canInteractDialogue
        {
            get
            {
                if (dialogues.Length != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public RoomTile(string name, Sprite sprite, int x, int y)
        {
            sOName = name;
            this.sprite = sprite;
            this.x = x;
            this.y = y;
        }
    }

    public void init(string name, int height, int width, float cellSizeX, float cellSizeY)
    {
        roomName = name;
        roomHeight = height;
        roomWidth = width;
        roomCellSizeX = cellSizeX;
        roomCellSizeY = cellSizeY;
    }

    public void AddLayer(Tilemap.SaveObject saveObject)
    {
        if (roomLayers == null)
            roomLayers = new List<RoomLayer>();

        RoomLayer currentRoomLayer = new RoomLayer(saveObject.layer, saveObject.location);
        List<RoomTile> tileList = new List<RoomTile>();

        foreach (var tilemap in saveObject.tilemapObjectSaveObjectArray)
        {
            if (!String.IsNullOrWhiteSpace(tilemap.sOName))
            {
                RoomTile currentTile = new RoomTile(tilemap.sOName, tilemap.sprite, tilemap.x, tilemap.y);
                tileList.Add(currentTile);
            }
        }

        currentRoomLayer.roomTiles = tileList;
        roomLayers.Add(currentRoomLayer);
    }
}

public enum RoomType
{
    Boss,
    Item,
    Normal,
    Secret,
    SuperSecret
}
