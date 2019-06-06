using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoricalCreationForm : CreationForm {
    public ItemListController optionsController;

    public override void Prepopulate(ConfigurationData option) {
        optionsController.AddItem(option);
    }

    public override string Validate() {
        return optionsController.listItems.Count < 1 ? "Please assign add at least two options to the list" : "OK";
    }

    public override void ClearFields() {
        optionsController.ClearItems();
    }

    public override ConfigurationData GetConfigurationData() {
        List<string> names = optionsController.GetLabelNames();
        return new CategoricalValue(optionsController.GetLabelNames());
    }


}

