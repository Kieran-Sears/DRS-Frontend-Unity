using UnityEngine.UI;

public class ActionCreationForm : CreationForm {
    public InputField nameInput;

    public ItemListController effects;

    public Button submitButton;
    public Button cancelButton;

    public void Start() {
        submitButton.onClick.AddListener(() => Submit());
        cancelButton.onClick.AddListener(() => cancelationDelegate());
        nameInput.onEndEdit.AddListener((string newName) => formFieldChangeDelegate(GetConfigurationData()));
    }

    private ConfigurationData GetConfigurationData() {
        return new ActionConfigurationData(
             label: nameInput.text,
             effectConfigurations: effects.GetData().ConvertAll((a) => a as EffectConfigurationData)
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
        ret = string.IsNullOrEmpty(nameInput.text) ? "Please assign an Action Name" : "OK";
        return ret;
    }

    public override void ClearFields() {
        nameInput.text = "";
        effects.ClearItems();
    }

    public override void Prepopulate(ConfigurationData data) {
        ActionConfigurationData action = data as ActionConfigurationData;
        nameInput.text = data.id;
        action.effectConfigurations.ForEach((attr) => effects.AddItem(attr));
    }

}


