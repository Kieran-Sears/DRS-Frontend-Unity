using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationHolder : ValueHolder
{
    public Configurations value;

    public override void SaveData(ConfigurationData data) {
        Debug.Log($"configuration data saving: {data.ToString()}");
        value = data as Configurations;
    }

    public override void UpdateDisplay(ConfigurationData data) {
        
    }
}
