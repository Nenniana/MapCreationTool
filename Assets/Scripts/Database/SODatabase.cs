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
}