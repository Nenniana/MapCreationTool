using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BrushSystem
{
    public static List<Vector3> BrushSize(Vector3 location, int size, float cellSizeX, float cellSizeY)
    {
        List<Vector3> tilemapObjectLocations = new List<Vector3>();

        tilemapObjectLocations.Add(location);

        if (size >= 2)
        {
            Debug.Log("Original location is: " + location);
            Debug.Log("Adding " + (location + new Vector3(-cellSizeX, 0)));
            tilemapObjectLocations.Add(location + new Vector3(-cellSizeX, 0));
        }

        if (size >= 4)
        {
            tilemapObjectLocations.Add(location + new Vector3(-cellSizeX, -cellSizeY));
            tilemapObjectLocations.Add(location + new Vector3(0, -cellSizeY));
        }

        if (size >= 9)
        {
            tilemapObjectLocations.Add(location + new Vector3(cellSizeX, 0));
            tilemapObjectLocations.Add(location + new Vector3(cellSizeX, cellSizeY));
            tilemapObjectLocations.Add(location + new Vector3(0, cellSizeY));
            tilemapObjectLocations.Add(location + new Vector3(cellSizeX, -cellSizeY));
            tilemapObjectLocations.Add(location + new Vector3(-cellSizeX, cellSizeY));
        }

        return tilemapObjectLocations;
    }
}
