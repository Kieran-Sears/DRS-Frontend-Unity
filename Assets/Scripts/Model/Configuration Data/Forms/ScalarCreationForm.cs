using UnityEngine.UI;
using static Enums;

public class ScalarCreationForm : CreationForm {

    public InputField min;
    public InputField max;
    public Dropdown variance;


    public override void Prepopulate(ConfigurationData data) {
        ScalarValue scalar = data as ScalarValue;
        min.text = scalar.min.ToString();
        max.text = scalar.max.ToString();
        variance.value = (int) scalar.variance;
    }


    public override string Validate(ConfigurationData data) {
        ScalarValue effect = data as ScalarValue;
        string ret = "OK";
        // TODO test and change around if need be:
        ret = Utilities.NumberValidation(effect.min, 0) ? ret : "Please assign a minimum value";
        ret = Utilities.NumberValidation(effect.max, 1) ? ret : "Please assign a maximum value";
        return ret;
    }

    public override void ClearFields() {
        min.text = "";
        max.text = "";
        variance.value = 0;
    }

    public override ConfigurationData GetData() {
        return new ScalarValue(int.Parse(min.text), int.Parse(max.text), (VarianceType)variance.value);
    }
}
