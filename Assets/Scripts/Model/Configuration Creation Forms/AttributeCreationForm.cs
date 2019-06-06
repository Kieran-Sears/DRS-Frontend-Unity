using UnityEngine;
using UnityEngine.UI;

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
        nameInput.text = attribute.id;
        switch (attribute.value.kind) {
            case "scalar":
                scalarCaller.AddItem(attribute.value);
                break;
            case "categorical":
                categoricalCaller.AddItem(attribute.value);
                break;
            default:
                break;
        }
    }

    public override string Validate() {
        string ret = "OK";
        ret = string.IsNullOrEmpty(nameInput.text) ? "Please assign a Attribute Name" : ret;
        ret = CreationManager.ATTRIBUTES.Find(x => x.id == Utilities.UpperFirst(nameInput.text)) != null ? "Please assign a unique Attribute Name" : ret;
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

    public override ConfigurationData GetConfigurationData() {
        //AttributeConfigurationData attribute = CreationManager.ATTRIBUTES.Find(x => x.id == UpperFirst(nameInput.text));
        //if (attribute == null) {
        Value value = null;
        value = scalarCaller.value == null ? value : scalarCaller.value;
        value = categoricalCaller.value == null ? value : categoricalCaller.value;
        AttributeConfigurationData attribute = new AttributeConfigurationData(Utilities.UpperFirst(nameInput.text), value);
        CreationManager.ATTRIBUTES.Add(attribute);
        //}
        return attribute;
    }
}
