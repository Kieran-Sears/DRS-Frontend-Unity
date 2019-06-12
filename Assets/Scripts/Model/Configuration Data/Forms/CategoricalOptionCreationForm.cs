using UnityEngine;
using UnityEngine.UI;

public class CategoricalOptionCreationForm : CreationForm {

    public InputField nameInput;
    public InputField probability;

    public void Start() {
        nameInput.onEndEdit.AddListener((string newName) => delegates.nameChangeDelegate(newName));
    }

    public override void Prepopulate(ConfigurationData data) {
        throw new System.NotImplementedException();
    }

    public override string Validate(ConfigurationData data) {
        CategoricalOptionConfigurationData option = data as CategoricalOptionConfigurationData;
        string ret = "OK";
        ret = string.IsNullOrEmpty(option.name) ? "Please assign a name to your category option" : "OK";
        ret = Utilities.NumberValidation(option.probability, 1, 100) ? "Please assign a proportion number between 1 and 100" : "OK";
        return ret;
    }

    public override void ClearFields() {
        nameInput.text = "";
    }

    public override ConfigurationData GetData() {
        CategoricalOptionConfigurationData option = CreationManager.OPTIONS.Find(x => x.name == Utilities.UpperFirst(nameInput.text));
        if (option == null) {
            option = new CategoricalOptionConfigurationData(Utilities.UpperFirst(nameInput.text), int.Parse(probability.text));
            CreationManager.OPTIONS.Add(option);
        }
        
        return option;
    }

}
