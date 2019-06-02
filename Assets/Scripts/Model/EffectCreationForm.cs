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

    public ConfigurationCreationForm simCreateForm;

    public void Start() {
        PopulateDropdowns();
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

    private void Submit() {
        string validation = Validate();
        if (validation == "OK") {
            Configurations c = GetConfigurationData() as Configurations;
            submissionDelegate(c);
        } else {
            errorMessage.DisplayError(validation);
        };
    }

    private string Validate() {
        string ret = "OK";
        ret = string.IsNullOrEmpty(nameInput.text) ? "Please assign an Effect Name" : ret;
        return ret;
    }

    private ConfigurationData GetConfigurationData() {
        return new EffectConfigurationData(nameInput.text, (ActionType)type.value, target.options[target.value].text);
    }

    private void PopulateDropdowns() {
        string[] actionTypeNames = Enum.GetNames(typeof(ActionType));
        type.AddOptions(new List<string>(actionTypeNames));

        List<string> attributes = new List<string>();
        foreach (CustomerConfigurationData customer in simCreateForm.customerController.GetData()) {
            foreach (AttributeConfigurationData item in customer.attributeConfigurations) {
                if (attributes.IndexOf(item.id) < 0) {
                    attributes.Add(item.id);
                }
            }
        }
        target.ClearOptions();
        target.AddOptions(attributes);
    }

    public override string ToString() {
        return base.ToString();
    }

}
