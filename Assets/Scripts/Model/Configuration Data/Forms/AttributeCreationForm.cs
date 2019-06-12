using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class AttributeCreationForm : CreationForm {
    public InputField nameInput;

    public AttributeValueController scalarCaller;
    public AttributeValueController categoricalCaller;

    public Text summaryTextbox;

    public void Start() {
        nameInput.onEndEdit.AddListener((string newName) => delegates.nameChangeDelegate(newName));
    }

    public override void Prepopulate(ConfigurationData data) {
        AttributeConfigurationData attribute = data as AttributeConfigurationData;
        nameInput.text = attribute.name;
        switch (attribute.value.kind) {
            case ConfigItemTypes.Scalar:
                scalarCaller.AddItem(attribute.value);
                break;
            case ConfigItemTypes.Categorical:
                categoricalCaller.AddItem(attribute.value);
                break;
            default:
                break;
        }
    }

    public override string Validate(ConfigurationData data) {
        AttributeConfigurationData attribute = data as AttributeConfigurationData;
        string ret = "OK";
        ret = string.IsNullOrEmpty(attribute.name) ? "Please assign a Attribute Name" : ret;
        return ret;
    }

    public override void ClearFields() {
        scalarCaller.value = null;
        categoricalCaller.value = null;
        nameInput.text = "";
        summaryTextbox.text = @"Please select whether this attribute is a scalar value or a categorical one below...\n\ni.e.\n\n
Categorical: A value which can be classified and put into a category of it's own amongst a set. e.g. Home-ownership: Mortgage, Private Rent, Council Housing, etc.\n
Scalar: A value which has an exact measure and falls within a continuous spectrum.e.g.Age: current(Optional) = 32, Minimum Age Range = 18, Maximum Age Range = 86.\n\n
(Note: An option to vary the value over time is also presented for scalar values)";
    }

    public override ConfigurationData GetData() {
        Value value = null;
        value = scalarCaller.value == null ? value : scalarCaller.value;
        value = categoricalCaller.value == null ? value : categoricalCaller.value;
        AttributeConfigurationData attribute = new AttributeConfigurationData(Utilities.UpperFirst(nameInput.text), value);
        return attribute;
    }
}
