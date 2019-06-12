using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectTrainingForm : MonoBehaviour {

    public new Text name;
    public Text attributeName;
    public Text attributeValue;
    public InputField value;
    public InputField deviation;
    public Button submit;
    public Button cancel;

    public void Start() {
        cancel.onClick.AddListener(() => {
            ClearFields();
            gameObject.SetActive(false);
        });
    }

    public void ClearFields() {
        name.text = "";
        value.text = "";
        attributeName.text = "";
        attributeValue.text = "";
        deviation.text = "";
    }

    public void Prepopulate(Effect effect, string attributeValue) {
        name.text = effect.name;
        attributeName.text = effect.target;
        this.attributeValue.text = attributeValue;

        if (!double.IsNaN(effect.value)) value.text = effect.value.ToString();
        if (!double.IsNaN(effect.deviation)) deviation.text = effect.deviation.ToString();
    }

    public string Validate(TrainingItem data) {
        throw new System.NotImplementedException();
    }

}
