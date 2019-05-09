using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class ScalarCreationForm : CreationForm
{
    public InputField startLabel;
    public InputField minLabel;
    public InputField maxLabel;
    public Dropdown varianceLabel;
    public Button submitButton;
    public Button cancelButton;

    public void Start() {
        cancelButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    override public void Display() {
        gameObject.SetActive(true);
        ClearFields();
        submitButton.onClick.AddListener(() => submissionDelegate(new ScalarValue(startLabel.text, minLabel.text, maxLabel.text, (VarianceType)varianceLabel.value)));
    }

    public override void ClearFields() {
        startLabel.text = "";
        minLabel.text = "";
        maxLabel.text = "";
        varianceLabel.value = 0;
    }

    public override void Prepopulate(ConfigurationData data) {
        ScalarValue scalar = data as ScalarValue;
        startLabel.text = scalar.start;
        minLabel.text = scalar.min;
        maxLabel.text = scalar.max;
        varianceLabel.value = (int) scalar.variance;
    }
}


public class ScalarValue : Value {

    public string start;
    public string min;
    public string max;
    public VarianceType variance;

    public ScalarValue(string start, string min, string max, VarianceType variance) {
        this.start = start;
        this.min = min;
        this.max = max;
        this.variance = variance;
    }

    public override string ToString() {
        return $"start: {start}\nmin: {min}\nmax: {max}\nvariance: {variance}";
    }

    public override string GetLabel() {
        return "ScalarValueLabel";
    }
}
