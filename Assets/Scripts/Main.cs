using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class Main : MonoBehaviour {

    public GameObject welcomePanel;
    public GameObject configurePanel;
    public GameObject trainPanel;
    public GameObject testPanel;
    public GameObject playPanel;

    void Start() {
        LoadWelcomeUI();
        // FOR TESTING API BACKEND:
        //AttributeConfigurationData a = new AttributeConfigurationData("arrears", new ScalarValue(0.0, 10.0, VarianceType.None));
        //CustomerConfigurationData c = new CustomerConfigurationData( "customer 1", 30, new List<AttributeConfigurationData> { a });
        //SimulationConfigurationData s = new SimulationConfigurationData("simulation 1", 0, 10, 100);
        //Configurations conf = new Configurations(s, new List<CustomerConfigurationData>{ c }, new List<ActionConfigurationData>());
        //StartCoroutine(NetworkManager.Instance.SendConfigurationRequest(conf));
    }

    public void LoadWelcomeUI() {
        configurePanel.SetActive(false);
        trainPanel.SetActive(false);
        testPanel.SetActive(false);
        playPanel.SetActive(false);
    }

    public void LoadTrainUI() {
        trainPanel.SetActive(true);
    }

    public void LoadTestUI() {
        testPanel.SetActive(true);
    }

    public void LoadPlayUI() {
        playPanel.SetActive(true);
    }
}