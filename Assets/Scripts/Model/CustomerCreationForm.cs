using UnityEngine;
using UnityEngine.UI;

public class CustomerCreationForm : CreationForm
{
    public InputField nameInput;
    public ItemListController attributeItemList;

    public Button submitButton;
    public Button cancelButton;

    public void Start() {
        cancelButton.onClick.AddListener(() => cancelationDelegate());
        submitButton.onClick.AddListener(() => Submit());
        nameInput.onEndEdit.AddListener((string newName) => formFieldChangeDelegate(GetCurrentValues()));
    }

    public override void ClearFields() {
        nameInput.text = "";
    }

    public override void Prepopulate(ConfigurationData data) {
        CustomerConfigurationData customer = data as CustomerConfigurationData;
        Debug.Log($"Temp Log 2:\n{data.ToString()}");
        nameInput.text = customer.id;
        customer.attributeConfigurations.ForEach((attr) => attributeItemList.AddItem(attr));
    }

    private void Submit() {
        string validation = Validate();
        if (validation == "OK") {
            submissionDelegate(GetCurrentValues());
        } else {
            errorMessage.DisplayError(validation);
        };
    }

    private ConfigurationData GetCurrentValues() {
        return new CustomerConfigurationData(
             label: nameInput.text,
             attributeConfigurations: attributeItemList.GetData().ConvertAll((a) => a as AttributeConfigurationData));
    }

    private string Validate() {
        return string.IsNullOrEmpty(nameInput.text) ? "Please assign a Customer Name" : "OK";
    }

}
