using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class TrainingData {
    public List<Customer> customers;
    public List<Action> actions;

    public TrainingData(List<Customer> customers, List<Action> actions) {
        this.customers = customers;
        this.actions = actions;
    }
}

public abstract class TrainingItemView : MonoBehaviour {

    public new Text name;
    public Toggle toggle;
    public Image background;

    public void ChangeToggleColor(bool t) {
        toggle.isOn = t;
        if (t) {
            background.color = new Color(255, 0, 0);
        } else {
            background.color = new Color(255, 255, 255);
        } 
    }

}

public abstract class TrainingItem {
    public string id;
    public string name;
    public ToggleGroup toggleGroup;
    public TrainingItemView view;
    public abstract void InitItemView(GameObject view, TrainingDelegateHolder delegates);

    public void OnToggleSelect(bool x, TrainingItemView itemView) {
        if (x) {
            foreach (Toggle toggle in toggleGroup.ActiveToggles()) {
                if (toggle != itemView.toggle)
                    toggle.isOn = false;
            }
        }
        itemView.ChangeToggleColor(x);
    }
}

public class Customer : TrainingItem {

    public double arrears;
    public double satisfaction;
    public List<Attribute> featureValues;

    public override void InitItemView(GameObject item, TrainingDelegateHolder delegates) {
        CustomerTrainingView view = item.GetComponent<CustomerTrainingView>();
        view.name.text = name;
        toggleGroup = item.transform.parent.GetComponent<ToggleGroup>();
        toggleGroup.RegisterToggle(view.toggle);

        view.toggle.onValueChanged.AddListener(x => {
            OnToggleSelect(x, view);
            delegates.selectionDelegate(this);
        });
        base.view = view;
    }

}

public class Attribute : TrainingItem {
    public double value;

    public override void InitItemView(GameObject item, TrainingDelegateHolder delegates) {
        AttributeTrainingView view = item.GetComponent<AttributeTrainingView>();
        view.name.text = name;
        view.value.text = value.ToString();
        toggleGroup = item.transform.parent.GetComponent<ToggleGroup>();
        toggleGroup.RegisterToggle(view.toggle);
        view.toggle.onValueChanged.AddListener(x => OnToggleSelect(x, view));
        base.view = view;
    }


}

public class Action : TrainingItem {

    public string target;
    public ActionType type;
    public List<Effect> effects;

    public Action(string name, string target, ActionType type, List<Effect> effects) {
        this.name = name;
        this.target = target;
        this.type = type;
        this.effects = effects;
    }

    public override void InitItemView(GameObject item, TrainingDelegateHolder delegates) {
        ActionTrainingView view = item.GetComponent<ActionTrainingView>();
        view.name.text = name;
        toggleGroup = item.transform.parent.GetComponent<ToggleGroup>();
        toggleGroup.RegisterToggle(view.toggle);
        view.toggle.onValueChanged.AddListener(x => {
            OnToggleSelect(x, view);
            delegates.selectionDelegate(this);
        });
        base.view = view;
    }

}

public class Effect : TrainingItem {
    public EffectType type;
    public string target;
    public double value = double.NaN;
    public double deviation = double.NaN;

    public Effect(string name, EffectType type, string target, double value, double deviation) {
        this.name = name;
        this.type = type;
        this.target = target;
        this.value = value;
        this.deviation = deviation;
    }

    public override void InitItemView(GameObject item, TrainingDelegateHolder delegates) {
        EffectTrainingView view = item.GetComponent<EffectTrainingView>();
        view.arrowButton.onClick.AddListener(() => delegates.selectionDelegate(this));
        base.view = view;
    }

}

