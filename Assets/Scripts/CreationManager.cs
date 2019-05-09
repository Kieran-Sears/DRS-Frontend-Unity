using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public abstract class CreationForm : MonoBehaviour {
    public abstract void Display();
    public abstract void ClearFields();
    public abstract void Prepopulate(ConfigurationData data);

    public delegate void SubmitConfigurationDataDelegate(ConfigurationData val);
    public SubmitConfigurationDataDelegate submissionDelegate;

    public delegate void ChangeListItemLabelDelegate(string val);
    public ChangeListItemLabelDelegate renameDelegate;
}

public class CreationManager : MonoBehaviour {

    public static CreationManager Instance { get; private set; }
    public GameObject listItemPrefab;
    public CustomerCreationForm customerCreationForm;
    public ActionCreationForm actionCreationForm;
    public AttributeCreationForm attributeCreationForm;
    public CategoricalCreationForm categoricalCreationForm;
    public ScalarCreationForm scalarCreationForm;

    public List<CustomerConfigurationData> customerConfigs = new List<CustomerConfigurationData>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private CreationForm GetForm(ConfigItemTypes type) {
        CreationForm form = null;
        switch (type) {
            case ConfigItemTypes.Customer:
                form = customerCreationForm;
                break;
            case ConfigItemTypes.Categorical:
                form = categoricalCreationForm;
                break;
            case ConfigItemTypes.Scalar:
                form = scalarCreationForm;
                break;
            case ConfigItemTypes.Attribute:
                form = attributeCreationForm;
                break;
            default:
                throw new System.Exception("ListItem type not yet implemented or added to ListItemTypes enum.");
        }
        return form;
    }

    public void LoadConfigurationItem(ListItemLabel label) {
        CreationForm form = GetForm(label.controller.type);
        form.Prepopulate(label.data);
        SetFormDelegates(form, label).Display();
    }

    public ListItemLabel CreateConfigurationItem(ItemListController itemListController, ConfigurationData data = null) {
        ListItemLabel label = Instantiate(listItemPrefab, itemListController.scrollList.transform).GetComponent<ListItemLabel>();
        label.controller = itemListController;
        if (data != null) {
            label.labelText.text = data.GetLabel();
            label.data = data;
        } else {
            SetFormDelegates(GetForm(itemListController.type), label).Display();
        }
        return label;
    }

    private CreationForm SetFormDelegates(CreationForm form, ListItemLabel label) {
        form.renameDelegate += label.UpdateText;
        form.submissionDelegate += label.SaveData;
        form.submissionDelegate += (x) => {
            form.renameDelegate -= label.UpdateText;
            form.ClearFields();
            form.gameObject.SetActive(false);
        };
        return form;
    }

}

