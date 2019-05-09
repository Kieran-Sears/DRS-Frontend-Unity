using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoricalCreationForm : CreationForm {
    public ItemListController itemList;

    public Button addButton;
    public Button removeButton;

    public Button cancelButton;
    public Button submitButton;

    public void Start() {
        cancelButton.onClick.AddListener(() => gameObject.SetActive(false));
        addButton.onClick.AddListener(() => itemList.AddItem());
    }

    override public void Display() {
        gameObject.SetActive(true);
        ClearFields();
        submitButton.onClick.AddListener(() => {
            string[] options = itemList.GetLabels();
            submissionDelegate(new CategoricalValue(new List<string>(options)));
        });
    }

    public override void Prepopulate(ConfigurationData data) {
        itemList.AddItem(data);
    }

    public override void ClearFields() {
        itemList.ClearItems();
    } 
}

public class CategoricalValue : Value {

    public List<string> options;

    public CategoricalValue(List<string> options) {
        this.options = options;
    }
    public override string ToString() {
        return $"options: {options}";
    }

    public override string GetLabel() {
        return "CategoricalValueLabel";
    }

}
