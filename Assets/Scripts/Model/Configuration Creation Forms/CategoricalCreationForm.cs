using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoricalCreationForm : CreationForm {
    public ItemListController itemList;

    public Button submitButton;
    public Button cancelButton;

    public void Start() {
        cancelButton.onClick.AddListener(() => cancelationDelegate());
        submitButton.onClick.AddListener(() => Submit());
    }

    public override void Prepopulate(ConfigurationData data) {
        itemList.AddItem(data);
    }

    public override void ClearFields() {
        itemList.ClearItems();
    }

    private void Submit() {
        string validation = Validate();
        if (validation == "OK") {
            submissionDelegate(GetConfigurationData());
        } else {
            errorMessage.DisplayError(validation);
        };
    }

    private ConfigurationData GetConfigurationData() {
        List<CategoricalOption> options = itemList.GetData().ConvertAll(x => x as CategoricalOption);
        return new CategoricalValue(new List<CategoricalOption>(options));
    }

    private string Validate() {
        return itemList.listItems.Count < 1 ? "Please assign add at least one option to the list" : "OK";
    }
}

