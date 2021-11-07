using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileVisualObject : MonoBehaviour
{
    private Vector3 location;
    private Sprite sprite;

    public event EventHandler<OnSpriteValueChangedEventArgs> OnSpriteValueChanged;
    public class OnSpriteValueChangedEventArgs : EventArgs
    {
        public Sprite sprite;
    }

    public void TriggerSpriteValueChanged(Sprite sprite)
    {
        if (OnSpriteValueChanged != null)
        {
            OnSpriteValueChanged(this, new OnSpriteValueChangedEventArgs
            {
                sprite = sprite
            });
        }
    }

    public void InitTileVisual ()
    {
        OnSpriteValueChanged += (object sender, OnSpriteValueChangedEventArgs eventArgs) =>
        {
            sprite = eventArgs.sprite;
        };
    }

    public void SetLocation (int x, int y)
    {
        location = new Vector3(x, y);
    }

    public Vector3 GetLocation ()
    {
        return location;
    }
}
