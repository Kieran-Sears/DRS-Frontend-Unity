using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class ItemListController : MonoBehaviour
{
    public GameObject scrollList;
    public ConfigItemTypes type;
    public List<ListItemLabel> listItems = new List<ListItemLabel>();

    public void AddItem() {
        Debug.Log("Creating Item " + type);
        listItems.Add(CreationManager.Instance.CreateConfigurationItem(this));
    }

    public void AddItem(ConfigurationData data) {
        Debug.Log("Creating Item " + type);
        listItems.Add(CreationManager.Instance.CreateConfigurationItem(this, data));
    }

    public void RemoveItem() {
       Toggle[] selectables = scrollList.GetComponentsInChildren<Toggle>();
        foreach (Toggle s in selectables) {
            if (s.isOn) {
                Destroy(s.gameObject);
            }
        }
    }

    public string[] GetLabels() {
       Text[] texts = scrollList.GetComponentsInChildren<Text>();
        string[] labels = new string[texts.Length];
        for (int i = 0; i < texts.Length; i++) {
            labels[i] = texts[i].text;
        }
        return labels;
    }

    public List<ConfigurationData> GetData() {
        return listItems.ConvertAll((x) => x.data);
    }

    public void ClearItems() {
        ListItemLabel[] items = scrollList.GetComponentsInChildren<ListItemLabel>();
        foreach (var item in items) {
            Destroy(item.gameObject);
        }

    }

    private string UpperFirst(string text) {
        return char.ToUpper(text[0]) +
            ((text.Length > 1) ? text.Substring(1).ToLower() : string.Empty);
    }

}
