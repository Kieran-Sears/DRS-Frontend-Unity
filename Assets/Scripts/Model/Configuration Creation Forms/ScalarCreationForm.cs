using UnityEngine.UI;
using static Enums;

public class ScalarCreationForm : CreationForm
{
    public InputField min;
    public InputField max;
    public Dropdown variance;


    public override void Prepopulate(ConfigurationData data) {
        ScalarValue scalar = data as ScalarValue;
        min.text = scalar.min.ToString();
        max.text = scalar.max.ToString();
        variance.value = (int) scalar.variance;
    }


    public override string Validate() {
        string ret = "OK";
        ret = Utilities.NumberValidation(min.text) ? "Please assign a minimum value" : ret;
        ret = Utilities.NumberValidation(max.text) ? "Please assign a maximum value" : ret;
        return ret;
    }

    public override void ClearFields() {
        min.text = "";
        max.text = "";
        variance.value = 0;
    }

    public override ConfigurationData GetConfigurationData() {
        return new ScalarValue(double.Parse(min.text), double.Parse(max.text), (VarianceType)variance.value);
    }
}
