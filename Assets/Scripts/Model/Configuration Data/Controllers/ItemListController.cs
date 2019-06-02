using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListController : FormCaller {
    public GameObject listItemPrefab;

    public GameObject scrollList;
    public List<ListItemLabel> listItems = new List<ListItemLabel>();

    public void AddItem() {
        ListItemLabel label = Instantiate(listItemPrefab, scrollList.transform).GetComponent<ListItemLabel>();
        label.controller = this;
        listItems.Add(CreationManager.Instance.CreateConfigurationItem(this, label) as ListItemLabel);
    }

    public void AddItem(ConfigurationData data) {
        ListItemLabel label = Instantiate(listItemPrefab, scrollList.transform).GetComponent<ListItemLabel>();
        label.controller = this;
        listItems.Add(CreationManager.Instance.CreateConfigurationItem(this, label, data) as ListItemLabel);
    }

    public void RemoveItem() {
       Toggle[] selectables = scrollList.GetComponentsInChildren<Toggle>();
        foreach (Toggle s in selectables) {
            if (s.isOn) {
                Destroy(s.gameObject);
            }
        }
    }

    public string[] GetLabels() {
       Text[] texts = scrollList.GetComponentsInChildren<Text>();
        string[] labels = new string[texts.Length];
        for (int i = 0; i < texts.Length; i++) {
            labels[i] = texts[i].text;
        }
        return labels;
    }

    public List<ConfigurationData> GetData() {
        return listItems.ConvertAll((x) => x.data);
    }

    public void ClearItems() {
        ListItemLabel[] items = scrollList.GetComponentsInChildren<ListItemLabel>();
        foreach (var item in items) {
            Destroy(item.gameObject);
        }
    }

    public override CreationForm SetFormDelegates(CreationForm form, ValueHolder holder) {
        form.cancelationDelegate += () => {
            form.formFieldChangeDelegate -= holder.UpdateDisplay;
            form.ClearFields();
            form.gameObject.SetActive(false);
            Destroy(holder.gameObject);
        };

        form.formFieldChangeDelegate += holder.UpdateDisplay;
        form.submissionDelegate += holder.SaveData;
        form.submissionDelegate += (x) => {
            form.formFieldChangeDelegate -= holder.UpdateDisplay;
            form.ClearFields();
            form.gameObject.SetActive(false);
        };
        return form;
    }

    private string UpperFirst(string text) {
        return char.ToUpper(text[0]) +
            ((text.Length > 1) ? text.Substring(1).ToLower() : string.Empty);
    }

}
