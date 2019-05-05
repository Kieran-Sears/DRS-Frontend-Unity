using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class ListItemLabel : MonoBehaviour
{
    public Text labelText;
    public ConfigurationData data;

    public void SaveData(ConfigurationData data) {
        this.data = data;
        Debug.Log($"Linked config to itemLabel");
    }

    public void UpdateText(string text) {
        labelText.text = text;
    }

}
