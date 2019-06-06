using UnityEngine;
using UnityEngine.UI;

public class CategoricalOptionCreationForm : CreationForm
{
    public InputField nameInput;
    public InputField proportionInput;

    public void Start() {
        nameInput.onEndEdit.AddListener((string newName) => delegates.nameChangeDelegate(newName));
    }

    public override void Prepopulate(ConfigurationData data) {
        throw new System.NotImplementedException();
    }

    public override string Validate() {
        string ret = "OK";
        ret = string.IsNullOrEmpty(nameInput.text) ? "Please assign a label to your category option" : "OK";
        ret = Utilities.NumberValidation(proportionInput.text, 1, 100) ? "Please assign a proportion number between 1 and 100" : "OK";
        return ret;
    }

    public override void ClearFields() {
        nameInput.text = "";
    }

    public override ConfigurationData GetConfigurationData() {
        CategoricalOptionConfigurationData option = CreationManager.OPTIONS.Find(x => x.id == Utilities.UpperFirst(nameInput.text));
        if (option == null) {
            option = new CategoricalOptionConfigurationData(Utilities.UpperFirst(nameInput.text), int.Parse(proportionInput.text));
            CreationManager.OPTIONS.Add(option);
        }
        
        return option;
    }

}
