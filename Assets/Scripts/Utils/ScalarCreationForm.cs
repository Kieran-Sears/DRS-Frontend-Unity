using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalarCreationForm : MonoBehaviour
{
    public InputField startLabel;
    public InputField minLabel;
    public InputField maxLabel;
    public Dropdown varianceLabel;
    public Button submitButton;

    public delegate void SubmitScalarDelegate(ScalarValue val);
    public SubmitScalarDelegate submitScalarDelegate;

    public void Display(SubmitScalarDelegate del) {
        gameObject.SetActive(true);
        submitScalarDelegate += del;
        submitScalarDelegate += (x) => gameObject.SetActive(false);
        submitButton.onClick.AddListener(() => submitScalarDelegate(new ScalarValue(startLabel.text, minLabel.text, maxLabel.text, varianceLabel.itemText.text)));
    }
}


public class ScalarValue : Value {

    public string start;
    public string min;
    public string max;
    public string variance;

    public ScalarValue(string start, string min, string max, string variance) {
        this.start = start;
        this.min = min;
        this.max = max;
        this.variance = variance;
    }

    public override string ToString() {
        return $"start: {start}\nmin: {min}\nmax: {max}\nvariance: {variance}";
    }
}
