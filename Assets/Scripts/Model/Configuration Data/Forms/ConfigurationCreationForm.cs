using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationCreationForm : CreationForm {
    public int minimumCustomers = 1;
    public int minumumActions = 2;
    public int minimumCustomersToGenerate = 10;
    public int minimumEndTime = 10;

    public InputField nameInput;

    public InputField startTime;
    public InputField endTime;
    public InputField numOfCustomers;

    public ItemListController customersController;
    public ItemListController actionsController;

    public override void Prepopulate(ConfigurationData data) {
        Configurations configurationData = data as Configurations;
        nameInput.text = configurationData.name;  
        foreach (CustomerConfigurationData customer in configurationData.customerConfigurations) {
            customersController.AddItem(customer);
        }
        foreach (ActionConfigurationData action in configurationData.actionConfigurations) {
            actionsController.AddItem(action);
        } 
    }

    public override string Validate(ConfigurationData data) {
        string ret = "OK";
        Configurations configuration = data as Configurations;
        ret = configuration.customerConfigurations.Count < minimumCustomers ? $"Please customer configurations ({minimumCustomers} minimum)" : ret;
        ret = configuration.actionConfigurations.Count < minumumActions ? $"Please add action configurations ({minumumActions} minimum)" : ret;
        ret = string.IsNullOrEmpty(configuration.simulationConfiguration.name) ? "Please assign a Configuration Name" : ret;
        ret = configuration.simulationConfiguration.endTime < minimumEndTime ? $"Please enter an end time ({minimumEndTime} minimum)" : ret;
        ret = configuration.simulationConfiguration.numberOfCustomers < minimumCustomersToGenerate ? $"Please enter the number of customers to be generated ({minimumCustomersToGenerate} minimum)" : ret;
        return ret;
    }

    public override void ClearFields() {
        customersController.ClearItems();
        actionsController.ClearItems();
        nameInput.text = "";
    }

    public override ConfigurationData GetData() {
        return new Configurations(
            new SimulationConfigurationData(Utilities.UpperFirst(nameInput.text), int.Parse(startTime.text), int.Parse(endTime.text), int.Parse(numOfCustomers.text)), 
            CreationManager.CUSTOMERS, 
            CreationManager.ACTIONS,
            CreationManager.EFFECTS,
            CreationManager.ATTRIBUTES,
            CreationManager.OPTIONS);
    }

    public void ActionTabFocus(RectTransform actionWorkspace) {
        if (customersController.listItems.Count > 0) {
            actionWorkspace.SetAsLastSibling();
        } else {
            errorMessage.DisplayError("Please Ensure at least one customer has been created first");
        }
    }
}
