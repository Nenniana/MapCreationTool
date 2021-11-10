using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SODatabase
{
    internal static Dictionary<TileType, List<ScriptableTileBase>> tiles;
    internal static Dictionary<TileType, List<ScriptableTileBase>> Tiles
    {
        get
        {
            if (tiles == null)
            {
                tiles = new Dictionary<TileType, List<ScriptableTileBase>>();

                List<ScriptableTileBase> tileObjects = Resources.LoadAll("SO/Tiles", typeof(ScriptableTileBase)).Cast<ScriptableTileBase>().ToList();
                foreach (ScriptableTileBase tileObject in tileObjects)
                {
                    if (!tiles.ContainsKey(tileObject.GetTileType()))
                        tiles.Add(tileObject.GetTileType(), new List<ScriptableTileBase>());

                    tiles[tileObject.GetTileType()].Add(tileObject);
                }
            }

            return tiles;
        }
    }

    internal static Dictionary<string, ScriptableRoomTemplate> saves;
    internal static Dictionary<string, ScriptableRoomTemplate> Saves
    {
        get
        {
            if (saves == null)
            {
                saves = new Dictionary<string, ScriptableRoomTemplate>();

                List<ScriptableRoomTemplate> tileObjects = Resources.LoadAll("Saves", typeof(ScriptableRoomTemplate)).Cast<ScriptableRoomTemplate>().ToList();
                foreach (ScriptableRoomTemplate tileObject in tileObjects)
                {
                    if (!saves.ContainsKey(tileObject.name))
                        saves.Add(tileObject.name, tileObject);

                }
            }

            return saves;
        }
    }

    internal static ScriptableRoomTemplate GetSaveByName (string name)
    {
        Debug.Log("SaveName was: " + name);

        foreach (var item in Saves)
        {
            Debug.Log("Names in database were" + item.Key);

            if (name == item.Key)
                Debug.Log("Match found!");
        }

        return Saves[name];
    }

    internal static List<string> GetAllSaveNames ()
    {
        List<string> names = new List<string>();

        foreach (var item in Saves)
        {
            names.Add(item.Key);
        }

        return names;
    }
}