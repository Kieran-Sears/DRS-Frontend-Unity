using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListController : TrainingController {

    private int counter = 0;

    public Text countText;
    public ScrollRect scrollView;
    public RectTransform content;

    public override void AddItems(List<TrainingItem> data) {
        delegates.selectionDelegate += SetCurrentlySelected;
        foreach (TrainingItem trainingData in data) {
            GameObject instance = Instantiate(prefab.gameObject, content);
            trainingData.InitItemView(instance, delegates);
            itemList.Add(trainingData);
            countText.text = counter.ToString();
            counter++;
        }   
    }

    public override void ClearItems() {
        foreach (Transform child in content) {
            Destroy(child.gameObject);
        }
        itemList.Clear();
        countText.text = "0";
        counter = 0;
    }

    public void SetCurrentlySelected(TrainingItem item) {
        currentlySelected = item;

    }
}
