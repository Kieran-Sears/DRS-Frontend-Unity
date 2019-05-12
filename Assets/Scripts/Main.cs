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


       // { "customer":[{"name":"Peter Payer","assignedLabel":0,"proportion":100,"attributes":[{"name":"arrears","value":{"startValue":0.0,"minRange":10.0,"variance":"None","maxRange":100.0,"kind":"scalar"}}],"appearance":"None","kind":"customer"}],"actions":[],"simulation":{"startTime":0,"numberOfCustomers":10,"kind":"simulation"}}

        AttributeConfigurationData a = new AttributeConfigurationData("arrears", new ScalarValue(0.0, 10.0, VarianceType.None));
        CustomerConfigurationData c = new CustomerConfigurationData( "customer 1", new List<AttributeConfigurationData> { a });
        SimulationConfigurationData s = new SimulationConfigurationData("simulation 1", 0, 10, 100);
     
        Configurations conf = new Configurations(s, new List<CustomerConfigurationData>{ c }, new List<ActionConfigurationData>());

        StartCoroutine(NetworkManager.Instance.SendConfigurationRequest(conf));
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