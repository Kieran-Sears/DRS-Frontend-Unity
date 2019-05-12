using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationHolder : ValueHolder
{
    public Configurations value;

    public override void SaveData(ConfigurationData data) {
        value = data as Configurations;
    }

    public override void UpdateDisplay(ConfigurationData data) {
        
    }
}
