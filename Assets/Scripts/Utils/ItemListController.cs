using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Enums;

public class ItemListController : FormCaller {
    public GameObject listItemPrefab;
    public GameObject scrollList;
    public List<ListItemLabel> listItems = new List<ListItemLabel>();

    public override void AddItem() {
        ListItemLabel label = Instantiate(listItemPrefab, scrollList.transform).GetComponent<ListItemLabel>();
        label.controller = this;
        listItems.Add(label);
        CreationManager.Instance.CreateConfigurationItem(this);
    }

    public void AddItem(ConfigurationData data) {
        ListItemLabel label = Instantiate(listItemPrefab, scrollList.transform).GetComponent<ListItemLabel>();
        label.controller = this;
        listItems.Add(label);
    }

    public List<string> GetLabelNames() {
        List<string> labels = new List<string>();
        foreach (ListItemLabel item in listItems) {
           labels.Add(item.label.text);
        }
        return labels;
    }

    public void ClearItems() {
        foreach (var item in listItems) {
            Destroy(item.gameObject);
        }
        listItems.Clear();
    }

    public override ConfigurationDelegateHolder SetFormDelegates(ConfigurationDelegateHolder delegates) {
        delegates.nameChangeDelegate += UpdateItemLabel;
        delegates.validationDelegate += DisallowDuplicates;
        delegates.submissionDelegate += CreationManager.SaveConfigurationItem;
        return delegates;
    }

    public void UpdateItemLabel(string newName) {
        listItems.Last().label.text = newName;
    }

    private string DisallowDuplicates(ConfigurationData data) {
       return listItems.Find(x => x.name == data.name) != null ? $"Please assign a unique {data.kind} Name" : "OK";
    }
}
