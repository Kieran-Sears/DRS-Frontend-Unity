using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListItemLabel : MonoBehaviour, IPointerDownHandler
{
    public Text label;
    public ItemListController controller;

    public void OnPointerDown(PointerEventData eventData) {
        if (eventData.clickCount == 2) {
            // TODO add way to load the creation form with prepopulated data from correct CreationManager pool
            switch (controller.type) {
                case Enums.ConfigItemTypes.Customer:
                    break;
                case Enums.ConfigItemTypes.Action:
                    break;
                case Enums.ConfigItemTypes.Attribute:
                    break;
                case Enums.ConfigItemTypes.Categorical:
                    break;
                case Enums.ConfigItemTypes.Scalar:
                    break;
                case Enums.ConfigItemTypes.CategoricalOption:
                    break;
                case Enums.ConfigItemTypes.Configuration:
                    break;
                case Enums.ConfigItemTypes.Effect:
                    break;
                default:
                    break;
            }
            eventData.clickCount = 0;
        }
    }

}
