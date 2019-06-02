using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeHolder : ValueHolder {
    
    public Value value;

    public override void SaveData(ConfigurationData data) {
        value = data as Value;
    }

    public override void UpdateDisplay(ConfigurationData data) {

    }
}