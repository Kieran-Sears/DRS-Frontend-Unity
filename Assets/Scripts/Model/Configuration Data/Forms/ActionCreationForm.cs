using System.Collections.Generic;
using UnityEngine.UI;
using static Enums;

public class ActionCreationForm : CreationForm {
    public InputField label;
    public Dropdown type;
    public ItemListController effectsController;

    public void Start() {
        PopulateDropdowns();
        label.onEndEdit.AddListener((string newName) => delegates.nameChangeDelegate(newName));
    }

    public override void Prepopulate(ConfigurationData data) {
        ActionConfigurationData action = data as ActionConfigurationData;
        label.text = data.name;
        action.effectConfigurations.ForEach((attr) => {
            EffectConfigurationData effect = CreationManager.EFFECTS.Find(x => x.name == data.name);
            effectsController.AddItem(effect);
        });
    }
 
    public override string Validate(ConfigurationData data) {
        ActionConfigurationData action = data as ActionConfigurationData;
        string ret = "OK";
        ret = string.IsNullOrEmpty(action.name) ? "Please assign an Action Name" : "OK";
        return ret;
    }

    public override void ClearFields() {
        label.text = "";
        type.value = 0;
        effectsController.ClearItems();
    }

    public override ConfigurationData GetData() {
        ActionConfigurationData action = CreationManager.ACTIONS.Find(x => x.name == Utilities.UpperFirst(label.text));
        if (action == null) {
            action = new ActionConfigurationData(
                label: Utilities.UpperFirst(label.text),
                type: (ActionType) type.value,
                effectConfigurations: effectsController.GetLabelNames()
                );
            CreationManager.ACTIONS.Add(action);
        }
        return action;
    }

    private void PopulateDropdowns() {
        string[] actionTypeNames = System.Enum.GetNames(typeof(ActionType));
        type.ClearOptions();
        type.AddOptions(new List<string>(actionTypeNames));
    }

}


