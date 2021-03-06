﻿using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;
using static Enums;
using System;

public class EffectCreationForm : CreationForm {

    public InputField nameInput;
    public Dropdown type;
    public Dropdown target;

    public void Start() {
        PopulateDropdowns();
        nameInput.onEndEdit.AddListener((string newName) => delegates.nameChangeDelegate(newName));
    }

    public override void Prepopulate(ConfigurationData data) {
      EffectConfigurationData effect = data as EffectConfigurationData;
      type.value = type.options.FindIndex((i) => { return i.text.Equals(effect.type); });
      target.value = target.options.FindIndex((i) => { return i.text.Equals(effect.type); });
    }

    public override string Validate(ConfigurationData data) {
        EffectConfigurationData effect = data as EffectConfigurationData;
        string ret = "OK";
        ret = string.IsNullOrEmpty(effect.name) ? "Please assign an Effect Name" : ret;
        return ret;
    }

    public override void ClearFields() {
        nameInput.text = "";
        type.value = 0;
        target.value = 0;
    }

    public override ConfigurationData GetData() {
        EffectConfigurationData effect = CreationManager.EFFECTS.Find(x => x.name == Utilities.UpperFirst(nameInput.text));
        if (effect == null) {
            effect = new EffectConfigurationData(Utilities.UpperFirst(nameInput.text), (EffectType)type.value, Utilities.UpperFirst(target.options[target.value].text));
            CreationManager.EFFECTS.Add(effect);
        }
        return effect;
    }

    private void PopulateDropdowns() {
        string[] effectTypeNames = Enum.GetNames(typeof(EffectType));
        type.ClearOptions();
        type.AddOptions(new List<string>(effectTypeNames));

        List<string> attributes = new List<string>();
        foreach (AttributeConfigurationData item in CreationManager.ATTRIBUTES) {
                attributes.Add(item.name);
        }
        target.ClearOptions();
        target.AddOptions(attributes.Union<string>(new List<string> { "Arrears", "Satisfaction" }).ToList());
    }

}
