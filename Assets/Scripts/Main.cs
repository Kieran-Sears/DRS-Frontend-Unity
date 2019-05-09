using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    public GameObject welcomePanel;
    public GameObject configurePanel;
    public GameObject trainPanel;
    public GameObject testPanel;
    public GameObject playPanel;

    void Start() {
        LoadWelcomeUI();   
    }

    public void LoadWelcomeUI() {
        configurePanel.SetActive(false);
        trainPanel.SetActive(false);
        testPanel.SetActive(false);
        playPanel.SetActive(false);
    }

    public void LoadConfigurationUI() {
        configurePanel.SetActive(true);
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