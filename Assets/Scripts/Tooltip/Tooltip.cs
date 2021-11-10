using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI headerField;

    [SerializeField]
    private TextMeshProUGUI contentField;

    [SerializeField]
    private LayoutElement layoutElement;

    [SerializeField]
    private int characterWrapLimit;

    public void SetText (string content, string header = "")
    {
        if(string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;

            layoutElement.enabled = (headerField.text.Length != 0 && headerField.text.Length > characterWrapLimit || contentField.text.Length != 0 && contentField.text.Length > characterWrapLimit) ? true : false;

    }

    private void Update()
    {
        Vector2 position = Input.mousePosition;

        transform.position = position;
    }
}
