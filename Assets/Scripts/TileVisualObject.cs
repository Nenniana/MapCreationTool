using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileVisualObject : MonoBehaviour
{
    private Vector3 location;
    

    public void SetLocation (int x, int y)
    {
        location = new Vector3(x, y);
    }
}
