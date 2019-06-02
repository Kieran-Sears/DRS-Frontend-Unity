using System.Collections.Generic;
using UnityEngine;
using static Enums;

public abstract class ValueHolder : MonoBehaviour {
    public abstract void UpdateDisplay(ConfigurationData data);
    public abstract void SaveData(ConfigurationData data);
}

public abstract class FormCaller : MonoBehaviour {
    public ConfigItemTypes type;
    public abstract CreationForm SetFormDelegates(CreationForm form, ValueHolder label);
}

public abstract class CreationForm : MonoBehaviour {
    public ErrorMessage errorMessage;

    public abstract void ClearFields();
    public abstract void Prepopulate(ConfigurationData data);

    public delegate void CancelConfigurationDataDelegate();
    public CancelConfigurationDataDelegate cancelationDelegate;

    public delegate void SubmitConfigurationDataDelegate(ConfigurationData val);
    public SubmitConfigurationDataDelegate submissionDelegate;

    public delegate void FormFieldChangeDelegate(ConfigurationData val);
    public FormFieldChangeDelegate formFieldChangeDelegate;
}

public class CreationManager : MonoBehaviour {

    public static CreationManager Instance { get; private set; }
    public GameObject listItemPrefab;
    public CustomerCreationForm customerCreationForm;
    public ActionCreationForm actionCreationForm;
    public AttributeCreationForm attributeCreationForm;
    public CategoricalCreationForm categoricalCreationForm;
    public CategoricalOptionCreationForm categoricalOptionCreationForm;
    public ScalarCreationForm scalarCreationForm;
    public ConfigurationCreationForm configurationCreationForm;
    public EffectCreationForm effectCreationForm;

    public List<CustomerConfigurationData> customerConfigs = new List<CustomerConfigurationData>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private CreationForm GetForm(ConfigItemTypes type, ConfigurationData prepopulate = null) {
        CreationForm form = null;
        switch (type) {
            case ConfigItemTypes.Customer:
                form = customerCreationForm;
                break;
            case ConfigItemTypes.Categorical:
                form = categoricalCreationForm;
                break;
            case ConfigItemTypes.Scalar:
                form = scalarCreationForm;
                break;
            case ConfigItemTypes.Attribute:
                form = attributeCreationForm;
                break;
            case ConfigItemTypes.CategoricalOption:
                form = categoricalOptionCreationForm;
                break;
            case ConfigItemTypes.Configuration:
                form = configurationCreationForm;
                break;
            case ConfigItemTypes.Action:
                form = actionCreationForm;
                break;
            case ConfigItemTypes.Effect:
                form = effectCreationForm;
                break;
            default:
                throw new System.Exception("ListItem type not yet implemented or added to ListItemTypes enum.");
        }

        if (prepopulate != null) {
            form.Prepopulate(prepopulate);
            
        }
        form.gameObject.SetActive(true);
        return form;
    }

    private ConfigurationData GetPrepopulateData(ConfigItemTypes type) {
        switch (type) {
            case ConfigItemTypes.Customer:
                AttributeConfigurationData arrears = new AttributeConfigurationData("Arrears", new ScalarValue(min: 50, max: 1000, variance: Enums.VarianceType.None));
                return new CustomerConfigurationData(null, 0, new List<AttributeConfigurationData>(new List<AttributeConfigurationData> { arrears }));
            default:
                return null;
        }
    }

    public ValueHolder CreateConfigurationItem(FormCaller caller, ValueHolder valueHolder, ConfigurationData data = null) {

        Debug.Log($"CreateConfigurationItem Type: {caller.type} Caller: {caller.gameObject.name} Holder: {valueHolder.ToString()}");

        CreationForm form;

        if (data != null) {
            valueHolder.UpdateDisplay(data);
            valueHolder.SaveData(data);
        } else {
            data = GetPrepopulateData(caller.type);
            form = GetForm(caller.type, data);
            caller.SetFormDelegates(form, valueHolder);
        }
        return valueHolder;
    }

}

