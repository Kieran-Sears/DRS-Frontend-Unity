using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListItemLabel : MonoBehaviour, IPointerDownHandler
{
    public Text labelText;
    public ConfigurationData data;
    public ItemListController controller;

    public void OnPointerDown(PointerEventData eventData) {
        if (eventData.clickCount == 2) {
            CreationManager.Instance.LoadConfigurationItem(this);
            eventData.clickCount = 0;
        }
    }

    public void SaveData(ConfigurationData data) {
        this.data = data;
        Debug.Log($"Linked config to itemLabel");
    }

    public void UpdateText(string text) {
        labelText.text = text;
    }

}
