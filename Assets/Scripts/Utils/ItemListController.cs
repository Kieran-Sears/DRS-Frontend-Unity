using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public override DelegateHolder SetFormDelegates(DelegateHolder delegates) {
        delegates.nameChangeDelegate += UpdateItemLabel;
        return delegates;
    }

    public void UpdateItemLabel(string newName) {
        listItems.Last().label.text = newName;
    }
}
