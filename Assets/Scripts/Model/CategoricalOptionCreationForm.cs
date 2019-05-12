using UnityEngine.UI;

public class CategoricalOptionCreationForm : CreationForm
{
    public InputField nameInput;

    public Button submitButton;
    public Button cancelButton;

    void Start() {
        nameInput.onEndEdit.AddListener((x) => formFieldChangeDelegate(GetCurrentValues()));
        cancelButton.onClick.AddListener(() => cancelationDelegate());
        submitButton.onClick.AddListener(() => Submit());
    }

    private void Submit() {
        string validation = Validate();
        if (validation == "OK") {
            submissionDelegate(new CategoricalOption(label: nameInput.text));
        } else {
            errorMessage.DisplayError(validation);
        };
    }

    private string Validate() {
        return string.IsNullOrEmpty(nameInput.text) ? "Please assign a label to your category option" : "OK";
    }

    public override void ClearFields() {
        nameInput.text = "";
    }

    public override void Prepopulate(ConfigurationData data) {
        throw new System.NotImplementedException();
    }

    private ConfigurationData GetCurrentValues() {
        return new CategoricalOption(nameInput.text);
    }

}
