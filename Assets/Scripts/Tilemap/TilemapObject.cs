using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapObject
{
    private int x;
    private int y;
    private GridBase<TilemapObject> gridBase;
    private Sprite sprite;
    private string sOName;

    public TilemapObject(GridBase<TilemapObject> gridBase, int x, int y)
    {
        this.gridBase = gridBase;
        this.x = x;
        this.y = y;
    }

    public void SetSO(Sprite sprite, string sOName)
    {
        this.sOName = sOName;
        this.sprite = sprite;
        gridBase.TriggerGridBaseObjectChanged(x, y);
        // Debug.Log("This is: " + x + " " + y);
    }

    public Sprite GetSprite ()
    {
        return sprite;
    }

    public override string ToString()
    {
        if (string.IsNullOrEmpty(sOName))
            return "Default";
        else
            return sOName;
    }

    [Serializable]
    public class SaveObject
    {
        public Sprite sprite;
        public string sOName;
        public int x;
        public int y;
    }

    public SaveObject Save()
    {
        // Debug.Log("TilemapObject Save function ran.");

        return new SaveObject
        {
            sprite = sprite,
            sOName = sOName,
            x = x,
            y = y,
        };
    }

    public void LoadScriptable(ScriptableRoomTemplate.RoomTile roomTile)
    {
        // Debug.Log("TilemapObject Load function ran.");

        sprite = roomTile.sprite;
        sOName = roomTile.sOName;
    }
}
