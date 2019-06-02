using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AttributeCreationForm : CreationForm {
    public InputField nameInput;

    public AttributeHolder valueHolder;

    public AttributeValueController scalarCaller;
    public AttributeValueController CategoricalCaller;

    public Text summaryTextbox;

    public Button submitButton;
    public Button cancelButton;

    public void Start() {
        submitButton.onClick.AddListener(() => Submit());
        cancelButton.onClick.AddListener(() => cancelationDelegate());
        nameInput.onEndEdit.AddListener((string newName) => formFieldChangeDelegate(GetConfigurationData()));
       
    }

    public override void ClearFields() {
        nameInput.text = "";
        summaryTextbox.text = @"Please select whether this attribute is a scalar value or a categorical one below...\n\ni.e.\n\n
Categorical: A value which can be classified and put into a category of it's own amongst a set. e.g. Home-ownership: Mortgage, Private Rent, Council Housing, etc.\n
Scalar: A value which has an exact measure and falls within a continuous spectrum.e.g.Age: current(Optional) = 32, Minimum Age Range = 18, Maximum Age Range = 86.\n\n
(Note: An option to vary the value over time is also presented for scalar values)";
    }

    public override void Prepopulate(ConfigurationData data) {
        AttributeConfigurationData attribute = data as AttributeConfigurationData;
        nameInput.text = attribute.id;
        Debug.Log($"Attempting to prepopulate data:\n{data.ToString()}");
        valueHolder.value = attribute.value;
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
        ret = string.IsNullOrEmpty(nameInput.text) ? "Please assign a Attribute Name" : ret;

        return ret;
    }

    private ConfigurationData GetConfigurationData() {
        AttributeConfigurationData attribute = CreationManager.attributes.Find(x => x.id == nameInput.text);

        if (attribute != null) {
            return attribute;
        } else {
            attribute = new AttributeConfigurationData(nameInput.text, valueHolder.value);
            CreationManager.attributes.Add(attribute);
            return attribute;
        }
    }
}
