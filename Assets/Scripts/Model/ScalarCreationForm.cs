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
        cancelButton.onClick.AddListener(() => cancelationDelegate());
        submitButton.onClick.AddListener(() => Submit());
    }

    public override void ClearFields() {
        startLabel.text = "";
        minLabel.text = "";
        maxLabel.text = "";
        varianceLabel.value = 0;
    }

    public override void Prepopulate(ConfigurationData data) {
        ScalarValue scalar = data as ScalarValue;
        startLabel.text = scalar.start.ToString();
        minLabel.text = scalar.min.ToString();
        maxLabel.text = scalar.max.ToString();
        varianceLabel.value = (int) scalar.variance;
    }

    private void Submit() {
        string validation = Validate();
        if (validation == "OK") {
            submissionDelegate(new ScalarValue(double.Parse(minLabel.text), double.Parse(maxLabel.text), (VarianceType)varianceLabel.value, double.Parse(startLabel.text)));
        } else {
            errorMessage.DisplayError(validation);
        };
    }

    private string Validate() {
        string ret = "OK";
        ret = string.IsNullOrEmpty(minLabel.text) ? "Please assign a Customer Name" : ret;
        ret = string.IsNullOrEmpty(maxLabel.text) ? "Please assign a Customer Name" : ret;
        return ret;
    }
}
