using UnityEngine;
using static Enums;
using System.Collections.Generic;

public class Main : MonoBehaviour {

    public GameObject welcomePanel;
    public GameObject configurePanel;
    public GameObject trainPanel;
    public GameObject testPanel;
    public GameObject playPanel;

    public ConfigurationController configure;
    public TrainingManager train; 


    private static Main _instance;
   

    public static Main Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<Main>();
                if (_instance == null) {
                    GameObject go = new GameObject();
                    go.name = typeof(Main).Name;
                    _instance = go.AddComponent<Main>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    void Start() {
        SetAllPanelsInactive();

        // FOR TESTING:
        ScalarValue arrearsScalar = new ScalarValue(0, 1000, VarianceType.None);
        ScalarValue satisfactionScalar = new ScalarValue(0, 100, VarianceType.None);

        CustomerConfigurationData customer1 = new CustomerConfigurationData("Customer1", 10, arrearsScalar, satisfactionScalar, new List<string> { "Arrears", "Satisfaction" });
        CustomerConfigurationData customer2 = new CustomerConfigurationData("Customer2", 10, arrearsScalar, satisfactionScalar, new List<string> { "Arrears", "Satisfaction" });

        SimulationConfigurationData simulation = new SimulationConfigurationData("simulation 1", 0, 10, 30);

        AttributeConfigurationData arrears = new AttributeConfigurationData("Arrears", arrearsScalar);
        AttributeConfigurationData satisfaction = new AttributeConfigurationData("Satisfaction", satisfactionScalar);

        ActionConfigurationData action1 = new ActionConfigurationData("PayOffDebt", ActionType.Customer, new List<string> { "ZeroArrears", "Satisfy", "Cooperative" });
        ActionConfigurationData action2 = new ActionConfigurationData("Litigate", ActionType.Agent, new List<string> { "ZeroArrears", "Dis-Satisfy" });

        EffectConfigurationData effect1 = new EffectConfigurationData("ZeroArrears", EffectType.Effect, "Arrears");
        EffectConfigurationData effect2 = new EffectConfigurationData("Satisfy", EffectType.Effect, "Satisfaction");
        EffectConfigurationData effect3 = new EffectConfigurationData("Cooperative", EffectType.Affect, "Satisfaction");
        EffectConfigurationData effect4 = new EffectConfigurationData("Dis-Satisfy", EffectType.Effect, "Satisfaction");

        Configurations conf = new Configurations(
            simulation, new List<CustomerConfigurationData> { customer1, customer2 },
            new List<ActionConfigurationData> { action1, action2 },
            new List<EffectConfigurationData> { effect1, effect2, effect3, effect4 },
            new List<AttributeConfigurationData> { arrears, satisfaction },
            new List<CategoricalOptionConfigurationData> { });

        LoadTrainUI(conf);
    }

    public void SetAllPanelsInactive() {
        configurePanel.SetActive(false);
        trainPanel.SetActive(false);
        testPanel.SetActive(false);
        playPanel.SetActive(false);
    }

    public void LoadTrainUI(ConfigurationData data) {
        Configurations configurations = data as Configurations;

        StartCoroutine(NetworkManager.Instance.SendConfigurationRequest(configurations, results => {
            train.Init(configurations, results);
        } ));

        SetAllPanelsInactive();
        trainPanel.SetActive(true);
    }

    public void LoadTestUI() {
        SetAllPanelsInactive();
        testPanel.SetActive(true);
    }

    public void LoadPlayUI() {
        SetAllPanelsInactive();
        playPanel.SetActive(true);
    }
}