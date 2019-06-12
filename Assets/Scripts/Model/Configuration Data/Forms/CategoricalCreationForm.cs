using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoricalCreationForm : CreationForm {

    public ItemListController optionsController;

    public override void Prepopulate(ConfigurationData option) {
        optionsController.AddItem(option);
    }

    public override string Validate(ConfigurationData data) {
        CategoricalValue categorical = data as CategoricalValue;
        return categorical.options.Count < 2 ? "Please add option configurations (2 minimum)" : "OK";
    }

    public override void ClearFields() {
        optionsController.ClearItems();
    }

    public override ConfigurationData GetData() {
        List<string> names = optionsController.GetLabelNames();
        return new CategoricalValue(optionsController.GetLabelNames());
    }


}

