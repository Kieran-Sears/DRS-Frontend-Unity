using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrainingItem {

    public TrainingItemView view;

    public abstract void InitItemView(GameObject view, int index);
}

public class Customer : TrainingItem {

    public string id;
    public double arrears;
    public double satisfaction;
    public List<FeatureValue> featureValues;

    public override void InitItemView(GameObject item, int index) {
        CustomerTrainingView itemView = item.GetComponent<CustomerTrainingView>();
        itemView.id.text = id;
        view = itemView;
    }

}

public class Attribute : TrainingItem {
    public override void InitItemView(GameObject item, int index) {
        AttributeTrainingView view = item.GetComponent<AttributeTrainingView>();
    }
}

//public class Action : TrainingItem {
//    public override void InitItemView(GameObject item, int index) {
//        CustomerTrainView view = item.GetComponent<CustomerTrainView>();
 
//    }
//}

//public class Metric : TrainingItem {
//    public override void InitItemView(GameObject item, int index) {
//        CustomerTrainView view = item.GetComponent<CustomerTrainView>();

//    }
//}

public class FeatureValue {
    public string id;
    public double value;
}