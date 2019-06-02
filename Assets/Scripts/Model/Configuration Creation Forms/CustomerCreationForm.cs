using UnityEngine;
using UnityEngine.UI;

public class CustomerCreationForm : CreationForm
{
    public InputField nameInput;
    public InputField proportionInput;

    public ItemListController attributeItemList;

    public Button submitButton;
    public Button cancelButton;

    public void Start() {
        submitButton.onClick.AddListener(() => Submit());
        cancelButton.onClick.AddListener(() => cancelationDelegate());
        nameInput.onEndEdit.AddListener((string newName) => formFieldChangeDelegate(GetConfigurationData()));
    }

    private ConfigurationData GetConfigurationData() {
        return new CustomerConfigurationData(
             label: nameInput.text,
             proportion: int.Parse(proportionInput.text),
             attributeConfigurations: attributeItemList.GetData().ConvertAll((a) => a as AttributeConfigurationData));
    }

    private void Submit() {
       string validation = Validate();
       if (validation == "OK") {
           submissionDelegate(GetConfigurationData());
       } else {
           errorMessage.DisplayError(validation);
       };
    }

    private string Validate() {
       string ret = "OK";
       ret = string.IsNullOrEmpty(nameInput.text) ? "Please assign a Customer Name" : "OK";
       ret = ProportionValidation(proportionInput.text) ? "Please assign a proportion number between 1 and 100" : "OK";
       return ret;
    }

    public override void ClearFields() {
        nameInput.text = "";
        proportionInput.text = "";
        attributeItemList.ClearItems();
    }

    public override void Prepopulate(ConfigurationData data) {
        CustomerConfigurationData customer = data as CustomerConfigurationData;
        nameInput.text = customer.id;
        proportionInput.text = customer.proportion.ToString();
        customer.attributeConfigurations.ForEach((attr) => attributeItemList.AddItem(attr));
    }

    private bool ProportionValidation(string text) {
        bool ret = true;
        int proportion = int.Parse(text);
        ret = string.IsNullOrEmpty(text) ? false : true;
        ret = proportion < 1 ? true : false;
        ret = proportion > 100 ? true : false;
        return ret;
    }

}
