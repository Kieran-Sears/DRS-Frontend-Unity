using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationCreationForm : CreationForm
{
    public InputField nameInput;

    public InputField startTime;
    public InputField endTime;
    public InputField numOfCustomers;

    public ItemListController customersController;
    public ItemListController actionsController;

    public override void Prepopulate(ConfigurationData data) {
        Configurations configurationData = data as Configurations;
        nameInput.text = configurationData.id;  
        foreach (CustomerConfigurationData customer in configurationData.customerConfigurations) {
            customersController.AddItem(customer);
        }
        foreach (ActionConfigurationData action in configurationData.actionConfigurations) {
            actionsController.AddItem(action);
        } 
    }

    public override string Validate() {
        string ret = "OK";
        ret = customersController.listItems.Count < 1 ? "Please add at least one customer" : ret;
        ret = actionsController.listItems.Count < 2 ? "Please add at least two actions" : ret;
        ret = string.IsNullOrEmpty(nameInput.text) ? "Please assign a Configuration Name" : ret;
        ret = string.IsNullOrEmpty(startTime.text) ? "Please enter a start time" : ret;
        ret = string.IsNullOrEmpty(endTime.text) ? "Please enter an end time" : ret;
        ret = string.IsNullOrEmpty(numOfCustomers.text) ? "Please enter the number of customers to be generated" : ret;
        return ret;
    }

    public override void ClearFields() {
        customersController.ClearItems();
        actionsController.ClearItems();
        nameInput.text = "";
    }

    public override ConfigurationData GetConfigurationData() {
        CreationManager.SIMULATION = new SimulationConfigurationData(Utilities.UpperFirst(nameInput.text), int.Parse(startTime.text), int.Parse(endTime.text), int.Parse(numOfCustomers.text));

        CreationManager.CONFIGURATION = new Configurations(
            CreationManager.SIMULATION, 
            CreationManager.CUSTOMERS, 
            CreationManager.ACTIONS,
            CreationManager.EFFECTS,
            CreationManager.ATTRIBUTES,
            CreationManager.OPTIONS);

        return CreationManager.CONFIGURATION;
    }

    public void ActionTabFocus(RectTransform actionWorkspace) {
        if (customersController.listItems.Count > 0) {
            actionWorkspace.SetAsLastSibling();
        } else {
            errorMessage.DisplayError("Please Ensure at least one customer has been created first");
        }
    }
}
