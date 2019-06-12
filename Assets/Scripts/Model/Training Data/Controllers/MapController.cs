using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class MapController : TrainingController {

    public void DrawLines(List<(AttributeTrainingView, ActionTrainingView, EffectType)> pairs) {
        for (int i = 0; i < pairs.Count; i++) {
            EffectTrainingView effectView = itemList[i] as EffectTrainingView;
            (AttributeTrainingView att, ActionTrainingView act, EffectType type) = pairs[i];
            effectView.LinkAttributeToAction(att, act, type);
        }    
    }

    public override void AddItems(List<TrainingItem> data) {
        foreach (TrainingItem trainingData in data) {
            GameObject instance = Instantiate(prefab.gameObject, transform);
            trainingData.InitItemView(instance, delegates);
            itemList.Add(trainingData.view);
        }
    }

    public override void ClearItems() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        itemList.Clear();
    }

}