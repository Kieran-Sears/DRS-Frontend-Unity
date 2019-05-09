using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerCreationForm : CreationForm
{
    public ScalarCreationForm arrears;
    public ErrorMessage errorMessage;

    public InputField nameInput;
    public ItemListController attributeItemList;

    public Button submitButton;
    public Button cancelButton;

    public void Start() {
        cancelButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    override public void Display() {
        gameObject.SetActive(true);
        ClearFields();
        nameInput.onEndEdit.AddListener((string newName) => renameDelegate(newName));
        submitButton.onClick.AddListener(() => {
            string validation = ValidateFields();
            if (validation == "OK") {
                submissionDelegate(new CustomerConfigurationData(
                label: nameInput.text,
                attributeConfigurations: attributeItemList.GetData().ConvertAll((a) => a as AttributeConfigurationData))); 
            } else {
                errorMessage.DisplayError(validation); 
            };
        });
    }

    public override void ClearFields() {
        nameInput.text = "";
    }

    public override void Prepopulate(ConfigurationData data) {
        CustomerConfigurationData customer = data as CustomerConfigurationData;
        nameInput.text = customer.GetLabel();
        customer.GetAttributeConfigurations().ForEach((attr) => attributeItemList.AddItem(attr));
    }

    private void AddDefaultArrears() {
       // CreationManager.Instance.CreateConfigurationItem(attributeItemList.transform, Enums.ConfigItemTypes.Attribute);
    }

    private string ValidateFields() {
        return string.IsNullOrEmpty(nameInput.text) ? "Please assign a Customer Name" : "OK";
    }

}
