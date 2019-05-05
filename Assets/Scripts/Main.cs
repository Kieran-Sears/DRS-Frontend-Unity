using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        welcomePanel.SetActive(true);
        configurePanel.SetActive(false);
        trainPanel.SetActive(false);
        testPanel.SetActive(false);
        playPanel.SetActive(false);
    }

    public void LoadConfigurationUI() {
        welcomePanel.SetActive(false);
        configurePanel.SetActive(true);
        trainPanel.SetActive(false);
        testPanel.SetActive(false);
        playPanel.SetActive(false);
    }

    public void LoadTrainUI() {
        welcomePanel.SetActive(false);
        configurePanel.SetActive(false);
        trainPanel.SetActive(true);
        testPanel.SetActive(false);
        playPanel.SetActive(false);
    }

    public void LoadTestUI() {
        welcomePanel.SetActive(false);
        configurePanel.SetActive(false);
        trainPanel.SetActive(false);
        testPanel.SetActive(true);
        playPanel.SetActive(false);
    }

    public void LoadPlayUI() {
        welcomePanel.SetActive(false);
        configurePanel.SetActive(false);
        trainPanel.SetActive(false);
        testPanel.SetActive(false);
        playPanel.SetActive(true);
    }
}