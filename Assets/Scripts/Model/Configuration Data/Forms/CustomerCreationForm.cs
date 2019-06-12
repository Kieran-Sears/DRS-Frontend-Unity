using UnityEngine;
using UnityEngine.UI;

public class CustomerCreationForm : CreationForm {
    public InputField nameInput;
    public InputField proportionInput;

    public AttributeValueController arrears;
    public AttributeValueController satisfaction;

    public ItemListController attributesController;

    public void Start() {
        nameInput.onEndEdit.AddListener((string newName) => delegates.nameChangeDelegate(newName));
    }

   public override void Prepopulate(ConfigurationData data) {
        CustomerConfigurationData customer = data as CustomerConfigurationData;
        nameInput.text = customer.name;
        proportionInput.text = customer.proportion.ToString();
        arrears.value = customer.arrears;
        satisfaction.value = customer.satisfaction;
        arrears.display.text = customer.arrears.ToString();
        satisfaction.display.text = customer.satisfaction.ToString();
        customer.attributeConfigurations.ForEach((attr) => {
            AttributeConfigurationData a = CreationManager.ATTRIBUTES.Find(x => x.name == attr);
            attributesController.AddItem(a); attributesController.UpdateItemLabel(a.name);
        });
    }

    public override string Validate(ConfigurationData data) {
        CustomerConfigurationData customer = data as CustomerConfigurationData;
        string ret = "OK";
        ret = string.IsNullOrEmpty(customer.name) ? "Please assign a Customer Name" : "OK";
        ret = Utilities.NumberValidation(customer.proportion, 1, 100) ? "Please assign a proportion number between 1 and 100" : "OK";
        ret = customer.arrears == null ? "Please assign a value to arrears" : "OK";
        ret = customer.satisfaction == null ? "Please assign a value to satisfaction" : "OK"; 
        return ret;
    }

    public override void ClearFields() {
        nameInput.text = "";
        proportionInput.text = "";
        attributesController.ClearItems();
    }

    public override ConfigurationData GetData() {
        CustomerConfigurationData customer = CreationManager.CUSTOMERS.Find(x => x.name == Utilities.UpperFirst(nameInput.text));

        if (customer == null) {
            customer = new CustomerConfigurationData(
                label: Utilities.UpperFirst(nameInput.text),
                proportion: int.Parse(proportionInput.text),
                arrears: arrears.value as ScalarValue,
                satisfaction: satisfaction.value as ScalarValue,
                attributeConfigurations: attributesController.GetLabelNames());
            CreationManager.CUSTOMERS.Add(customer);
        }
        return customer;
    }

}
