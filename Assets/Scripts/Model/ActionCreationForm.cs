using UnityEngine.UI;

public class ActionCreationForm : CreationForm {
    public InputField nameInput;

    public ItemListController effectingAttributes;
    public ItemListController affectingAttributes;
    public ItemListController effectingMetrics;

    public Button submitButton;
    public Button cancelButton;

    public void Start() {
        cancelButton.onClick.AddListener(() => cancelationDelegate());
        submitButton.onClick.AddListener(() => Submit());
        nameInput.onEndEdit.AddListener((string newName) => formFieldChangeDelegate(GetConfigurationData()));
    }

    private ConfigurationData GetConfigurationData() {
        return new ActionConfigurationData(
             label: nameInput.text,
             effectConfigurations: effectingAttributes.GetData().ConvertAll((a) => a as EffectConfigurationData),
             affectingConfigurations: affectingAttributes.GetData().ConvertAll((a) => a as EffectConfigurationData),
             metricConfigurations: effectingMetrics.GetData().ConvertAll((a) => a as EffectConfigurationData)
             );
    }

    private void Submit() {
        string validation = Validate();
        if (validation == "OK") {
            submissionDelegate(GetConfigurationData());
        } else {
            errorMessage.DisplayError(validation);
        };
    }

    private string Validate() {
        string ret = "OK";
        ret = string.IsNullOrEmpty(nameInput.text) ? "Please assign a Customer Name" : "OK";
        return ret;
    }

    public override void ClearFields() {
        nameInput.text = "";
        effectingAttributes.ClearItems();
        affectingAttributes.ClearItems();
        effectingMetrics.ClearItems();
    }

    public override void Prepopulate(ConfigurationData data) {
        ActionConfigurationData action = data as ActionConfigurationData;
        nameInput.text = data.id;
        action.effectConfigurations.ForEach((attr) => effectingAttributes.AddItem(attr));
        action.affectingConfigurations.ForEach((attr) => affectingAttributes.AddItem(attr));
        action.metricConfigurations.ForEach((attr) => effectingMetrics.AddItem(attr));
    }

}


