using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CreationManager;
using static Enums;

public class CreationManager : MonoBehaviour {

    public static CreationManager Instance { get; private set; }
    public GameObject listItemPrefab;
    public CustomerCreationForm customerCreationForm;
    public ActionCreationForm actionCreationForm;
    public AttributeCreationForm attributeCreationForm;

    public List<CustomerConfigurationData> customerConfigs = new List<CustomerConfigurationData>(); 

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void CreateConfigurationItem(Transform itemListTransform, ConfigItemTypes type) {
        switch (type) {
            case ConfigItemTypes.CUSTOMER:
                Debug.Log("Customer Creation Initialised...");
                GameObject customerListItem = Instantiate(listItemPrefab, itemListTransform);
                ListItemLabel customerItemLabel = customerListItem.GetComponent<ListItemLabel>();
                customerCreationForm.Display(customerItemLabel);
                break;
            case ConfigItemTypes.ATTRIBUTE:
                Debug.Log("Attribute Creation Initialised...");
                GameObject attributeListItem = Instantiate(listItemPrefab, itemListTransform);
                ListItemLabel attributeItemLabel = attributeListItem.GetComponent<ListItemLabel>();
                attributeCreationForm.Display(attributeItemLabel);
                break;
            default:
                throw new System.Exception("ListItem type not yet implemented or added to ListItemTypes enum.");
        }

    }

}


//    public interface ConfigurationItem {
//        void DisplayCreationForm(ListItemLabel label);
//    }

//[System.Serializable]
//public class CustomerConfiguration : ConfigurationItem {

//    public CustomerCreationForm creationUI;
    
 
//    public CustomerConfiguration(Transform itemListTransform, CustomerCreationForm form) {
//            creationUI = form;
//        }

//    public void DisplayCreationForm(ListItemLabel label) {
//        creationUI.Display(label);
//    }
//}


//[System.Serializable]
//public class AttributeConfiguration : ConfigurationItem {
//    public AttributeCreationForm creationUI;
  

//    public AttributeConfiguration(Transform itemListTransform, AttributeCreationForm form) {
//        creationUI = form;
//    }

//    public void DisplayCreationForm(ListItemLabel label) {
//        creationUI.Display(label);
//    }
//}

//    [System.Serializable]
//    public class ActionConfiguration : ConfigurationItem {

//        public ActionCreationForm creationUI;
//        public ActionConfigurationData data;
//        public ConfigItemTypes type;


//        public ActionConfiguration(Transform itemListTransform, ActionCreationForm form) {
//            creationUI = form;
//            type = ConfigItemTypes.ACTION;
//        }

//        public void DisplayCreationForm(ListItemLabel label) {
//        Debug.Log("displaying Action creation form");
//        creationUI.Display(label);
//        }

//        public void SaveItem(ActionConfigurationData data) { }

//}