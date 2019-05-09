using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AttributeCreationForm : CreationForm {
    public InputField nameInput;
    public Value value;

    public Button scalarButton;
    public Button categoricalButton;
  
    public ScalarCreationForm scalarForm;
    public CategoricalCreationForm categoricalForm;

    public Text summaryTextbox;

    public Button submitButton;
    public Button cancelButton;



    public void Start() {
        cancelButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    override public void Display() {
        ClearFields();
        gameObject.SetActive(true);
    
        nameInput.onEndEdit.AddListener((string newName) => renameDelegate(newName));

        submitButton.onClick.AddListener(() => {
            submissionDelegate(new AttributeConfigurationData(nameInput.text, value));
        });
    }

    public override void ClearFields() {
        nameInput.text = "";
        value = null;
        summaryTextbox.text = @"Please select whether this attribute is a scalar value or a categorical one below...\n\ni.e.\n\n
Categorical: A value which can be classified and put into a category of it's own amongst a set. e.g. Home-ownership: Mortgage, Private Rent, Council Housing, etc.\n
Scalar: A value which has an exact measure and falls within a continuous spectrum.e.g.Age: current(Optional) = 32, Minimum Age Range = 18, Maximum Age Range = 86.\n\n
(Note: An option to vary the value over time is also presented for scalar values)";
    }

    public override void Prepopulate(ConfigurationData data) {
        AttributeConfigurationData attribute = data as AttributeConfigurationData;
        nameInput.text = attribute.GetLabel();
        value = attribute.GetValue();
    }

    public void CreateScalar() {
        scalarForm.submissionDelegate += GetValue;
        scalarForm.submissionDelegate += (x) => scalarForm.gameObject.SetActive(false);
        scalarForm.Display();
    }

    public void CreateCategorical() {
        categoricalForm.submissionDelegate += GetValue;
        categoricalForm.submissionDelegate += (x) => categoricalForm.gameObject.SetActive(false);
        categoricalForm.Display();
    }

    public void GetValue(ConfigurationData value) {
        this.value = value as Value;
        summaryTextbox.text = value.ToString();
    }
}
