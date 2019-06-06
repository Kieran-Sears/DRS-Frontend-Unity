using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class DelegateHolder {
    public delegate void CancelConfigurationDataDelegate();
    public CancelConfigurationDataDelegate cancelationDelegate = null;

    public delegate void SubmitConfigurationDataDelegate(ConfigurationData val);
    public SubmitConfigurationDataDelegate submissionDelegate = null;

    public delegate void FormFieldChangeDelegate(string newName);
    public FormFieldChangeDelegate nameChangeDelegate = null;

    public void ClearAllDelegates() {
        cancelationDelegate = null;
        submissionDelegate = null;
        nameChangeDelegate = null;
    }
}

public abstract class FormCaller : MonoBehaviour {

    public Button AddButton;

    public void Start() {
        AddButton.onClick.AddListener(() => AddItem());
    }

    public ConfigItemTypes type;
    public abstract void AddItem();
    public abstract DelegateHolder SetFormDelegates(DelegateHolder delegates);
}

public static class Utilities {

    public static string UpperFirst(string text) {
        return char.ToUpper(text[0]) +
            ((text.Length > 1) ? text.Substring(1).ToLower() : string.Empty);
    }

    public static bool NumberValidation(string text, int min = -1, int max = -1) {
        bool ret = true;
        int proportion = int.Parse(text);
        ret = string.IsNullOrEmpty(text);
        if (min != -1) {
            ret = proportion < 1;
        }
        if (max != -1) {
            ret = proportion > 100;
        }
        return ret;
    }
}

public abstract class CreationForm : MonoBehaviour {
    public Button submitButton;
    public Button cancelButton;
    public FormCaller caller;
    public ErrorMessage errorMessage;
    public DelegateHolder delegates = new DelegateHolder();

    void OnDisable() {
        delegates.ClearAllDelegates();
        submitButton.onClick.RemoveListener(Submit);
    }

    void OnEnable() {
        delegates = caller.SetFormDelegates(delegates);
        delegates.submissionDelegate += x => gameObject.SetActive(false);
        submitButton.onClick.AddListener(Submit);
    }

    public void Submit() {
        string validation = Validate();
        if (validation == "OK") {
            ConfigurationData data = GetConfigurationData();
            Debug.Log($"Submit added:\n     {this.gameObject.name}\n\nCreationConfiguration:\n    {data}");
            delegates.submissionDelegate(data);
            gameObject.SetActive(false);
        } else {
            errorMessage.DisplayError(validation);
        };
    }

    public abstract void ClearFields();
    public abstract void Prepopulate(ConfigurationData data);
    public abstract ConfigurationData GetConfigurationData();
    public abstract string Validate();
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

    public static Configurations CONFIGURATION;
    public static SimulationConfigurationData SIMULATION;
    public static List<AttributeConfigurationData> ATTRIBUTES = new List<AttributeConfigurationData>();
    public static List<CategoricalOptionConfigurationData> OPTIONS = new List<CategoricalOptionConfigurationData>();
    public static List<CustomerConfigurationData> CUSTOMERS = new List<CustomerConfigurationData>();
    public static List<ActionConfigurationData> ACTIONS = new List<ActionConfigurationData>();
    public static List<EffectConfigurationData> EFFECTS = new List<EffectConfigurationData>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private CreationForm GetForm(FormCaller caller, ConfigurationData prepopulate = null) {
        CreationForm form = null;
        switch (caller.type) {
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
        form.ClearFields();
        form.caller = caller;
        if (prepopulate != null) {
            form.Prepopulate(prepopulate);
        }
        form.gameObject.SetActive(true);
        return form;
    }

    private ConfigurationData GetPrepopulateData(ConfigItemTypes type) {
        switch (type) {
            case ConfigItemTypes.Customer:
                // TODO?
                //if (ATTRIBUTES.Find(x => x.id == "Arrears") == null) {
                //    ATTRIBUTES.Add(new AttributeConfigurationData("Arrears", null));
                //}
                //if (ATTRIBUTES.Find(x => x.id == "Satisfaction") == null) {
                //    ATTRIBUTES.Add(new AttributeConfigurationData("Satisfaction", null));
                //}
                return new CustomerConfigurationData(null, 0, new ScalarValue(min: 50, max: 1000, variance: VarianceType.None), new ScalarValue(min: 0, max: 100, variance: VarianceType.None), new List<string>());
            default:
                return null;
        }
    }

    public void CreateConfigurationItem(FormCaller caller, ConfigurationData data = null) {
        // Debug.Log($"CreateConfigurationItem Type: {caller.type} Caller: {caller.gameObject.name}");
        CreationForm form = null;
        if (data == null) {
            data = GetPrepopulateData(caller.type);
        }
        form = GetForm(caller, data);
    }

}

