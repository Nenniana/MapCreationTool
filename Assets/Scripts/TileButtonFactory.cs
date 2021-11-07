using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileButtonFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject buttonScrollList;

    // Start is called before the first frame update
    void Start()
    {
        buttonScrollList.SetActive(true);

        foreach (KeyValuePair<TileType, List<ScriptableTileBase>> entry in SODatabase.Tiles)
        {
            //Debug.Log("Name:" + entry.Key);

            foreach (ScriptableTileBase scriptableTileBase in entry.Value)
            {
                //Debug.Log("Type:" + scriptableTileBase.GetTileType());
                GameObject currentButton = Instantiate(button);
                currentButton.transform.SetParent(this.transform);
                currentButton.GetComponent<TileButton>().InitButton(scriptableTileBase.GetPrefabSprite(), scriptableTileBase.GetPrefabAbbr(), scriptableTileBase.name);
            }
        }
    }
}
