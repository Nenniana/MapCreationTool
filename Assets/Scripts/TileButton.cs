using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Testing testing;

    [SerializeField]
    private TMPro.TextMeshProUGUI tMPro;

    [SerializeField]
    private Image image;

    private string sOName;
    private string sOAbbr;
    private Sprite sprite;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(sOAbbr, sOName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
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
        this.sOAbbr = sOAbbr;
        this.image.sprite = sprite;
        tMPro.text = sOName;
    }

    private void SetSO ()
    {
        testing.SetSO(sprite, sOName);
    }
}

