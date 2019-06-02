using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListItemLabel : ValueHolder, IPointerDownHandler
{
    public Text labelText;
    public ConfigurationData data;
    public ItemListController controller;

    public void OnPointerDown(PointerEventData eventData) {
        if (eventData.clickCount == 2) {
            Debug.Log($"Loading configuration item:\n{data.ToString()}");
            CreationManager.Instance.CreateConfigurationItem(controller, this, data);
            eventData.clickCount = 0;
        }
    }

    public override void SaveData(ConfigurationData data) {
        this.data = data;
    }

    public override void UpdateDisplay(ConfigurationData text) {
       
        labelText.text = text.id;
    }

}
