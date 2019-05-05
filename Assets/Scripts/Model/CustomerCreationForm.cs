using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerCreationForm : MonoBehaviour
{

    public InputField nameInput;
    public ScalarCreationForm arrears;
    public GameObject attributeItemList;
    public Button submitButton;

    public delegate void SubmitCustomerDelegate(CustomerConfigurationData data);
    public SubmitCustomerDelegate submitCustomerDelegate;

    public delegate void NameChangeDelegate(string name);
    public NameChangeDelegate nameChangeDelegate;

    public void Display(ListItemLabel itemLabel) {
        gameObject.SetActive(true);
        nameChangeDelegate += itemLabel.UpdateText;
        nameInput.onEndEdit.AddListener((string newName) => nameChangeDelegate(newName));

        submitCustomerDelegate += itemLabel.SaveData;
        submitCustomerDelegate += (x) => {
            gameObject.SetActive(false);
            Debug.Log("Customer Creation Complete, Saving Data");
        };

        submitButton.onClick.AddListener(() => submitCustomerDelegate(new CustomerConfigurationData(
            averageArrears: arrears.startLabel.text,
            minRangeArrears: arrears.minLabel.text,
            maxRangeArrears: arrears.maxLabel.text,
            variationOnArrears: arrears.varianceLabel.captionText.text,
            attributeConfigurations: GetAttributeData())));
    }

    private AttributeConfigurationData[] GetAttributeData() {
       ListItemLabel[] listItems = attributeItemList.GetComponentsInChildren<ListItemLabel>();
        AttributeConfigurationData[] attributeConfigs = new AttributeConfigurationData[listItems.Length];
        for (int i = 0; i < listItems.Length; i++) {
            attributeConfigs[i] = listItems[i].data as AttributeConfigurationData;
        }
       return attributeConfigs;
    }

}
