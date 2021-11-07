using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Testing testing;

    private string sOName;
    private Sprite sprite;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("I'm being hovered: " + sOName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("I'm no longer being hovered: " + sOName);
    }

    private void Start()
    {
        testing = GameObject.FindGameObjectWithTag("TestingScript").GetComponent<Testing>();
        this.GetComponent<Button>().onClick.AddListener(SetSO);
    }


    public void InitButton(Sprite sprite, string sOAbbr, string sOName)
    {
        this.sprite = sprite;
        this.sOName = sOName;
        this.GetComponent<Image>().sprite = sprite;
        this.GetComponentInChildren<Text>().text = sOAbbr;
    }

    private void SetSO ()
    {
        testing.SetSO(sprite, sOName);
    }
}

