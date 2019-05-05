using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class ItemListController : MonoBehaviour
{
    public GameObject scrollList;
    public ConfigItemTypes type;

    public void AddItem() {
        Debug.Log("Creating Item " + type);
        CreationManager.Instance.CreateConfigurationItem(scrollList.transform, type);
    }

    public void RemoveItem() {
       Toggle[] selectables = scrollList.GetComponentsInChildren<Toggle>();
        foreach (Toggle s in selectables) {
            if (s.isOn) {
                Destroy(s.gameObject);
            }
        }
    }

    private string UpperFirst(string text) {
        return char.ToUpper(text[0]) +
            ((text.Length > 1) ? text.Substring(1).ToLower() : string.Empty);
    }

}
