using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;
using static Enums;
using System;

public class EffectCreationForm : CreationForm {

    public InputField nameInput;
    public Dropdown type;
    public Dropdown target;

    public Button cancelButton;
    public Button submitButton;

    public void Start() {
        PopulateDropdowns();
        submitButton.onClick.AddListener(() => Submit());
        cancelButton.onClick.AddListener(() => cancelationDelegate());
        nameInput.onEndEdit.AddListener((string newName) => formFieldChangeDelegate(GetConfigurationData()));
    }

    private ConfigurationData GetConfigurationData() {
        return new EffectConfigurationData(nameInput.text, (ActionType)type.value, target.options[target.value].text);
    }

    private void Submit() {
        string validation = Validate();
        if (validation == "OK") {
            EffectConfigurationData data = GetConfigurationData() as EffectConfigurationData;
            submissionDelegate(data);
        } else {
            errorMessage.DisplayError(validation);
        };
    }

    private string Validate() {
        string ret = "OK";
        ret = string.IsNullOrEmpty(nameInput.text) ? "Please assign an Effect Name" : ret;
        return ret;
    }

    public override void ClearFields() {
        type.value = 0;
        target.value = 0;
    }

    public override void Prepopulate(ConfigurationData data) {
      EffectConfigurationData effect = data as EffectConfigurationData;
      type.value = type.options.FindIndex((i) => { return i.text.Equals(effect.type); });
      target.value = target.options.FindIndex((i) => { return i.text.Equals(effect.type); });
    }

    private void PopulateDropdowns() {
        string[] actionTypeNames = Enum.GetNames(typeof(ActionType));
        type.AddOptions(new List<string>(actionTypeNames));

        List<string> attributes = new List<string>();
        
        foreach (AttributeConfigurationData item in CreationManager.attributes) {
                attributes.Add(item.id);
        }
        
        target.ClearOptions();
        target.AddOptions(attributes);
    }

    public override string ToString() {
        return base.ToString();
    }

}
