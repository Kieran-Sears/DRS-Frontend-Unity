using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationCreationForm : CreationForm
{
    public InputField nameInput;

    public InputField startTime;
    public InputField endTime;
    public InputField numOfCustomers;

    public ItemListController customerController;
    public ItemListController actionController;

    public Button submitButton;

    public void Start() {
        submitButton.onClick.AddListener(() => Submit());
    }

    public override void ClearFields() {
        customerController.ClearItems();
        actionController.ClearItems();
        nameInput.text = "";
    }

    public override void Prepopulate(ConfigurationData data) {
        Debug.Log($"Prepopulating Configuration data:\n{data.ToString()}");
        Configurations configurationData = data as Configurations;
        nameInput.text = configurationData.id;
        List<CustomerConfigurationData> customers = configurationData.customerConfigurations;
        foreach (CustomerConfigurationData customer in customers) {
            customerController.AddItem();
        }
        List<ActionConfigurationData> actions = configurationData.actionConfigurations;
        foreach (ActionConfigurationData action in actions) {
            actionController.AddItem(action);
        } 
    }

    private void Submit() {
        string validation = Validate();
        if (validation == "OK") {
            Configurations c = GetConfigurationData() as Configurations;
           submissionDelegate(c);
        } else {
            errorMessage.DisplayError(validation);
        };
    }

    public void ActionTabFocus(RectTransform actionWorkspace) {
        if (customerController.listItems.Count > 0) {
            actionWorkspace.SetAsLastSibling();
        } else {
            errorMessage.DisplayError("Please Ensure at least one customer has been created first");
        }
    }

    private string Validate() {
        string ret = "OK";
        ret = customerController.listItems.Count < 1 ? "Please add at least one customer" : ret;
        ret = actionController.listItems.Count < 2 ? "Please add at least two actions" : ret;
        ret = string.IsNullOrEmpty(nameInput.text) ? "Please assign a Configuration Name" : ret;
        ret = string.IsNullOrEmpty(startTime.text) ? "Please enter a start time" : ret;
        ret = string.IsNullOrEmpty(endTime.text) ? "Please enter an end time" : ret;
        ret = string.IsNullOrEmpty(numOfCustomers.text) ? "Please enter the number of customers to be generated" : ret;
        return ret;
    }

    private ConfigurationData GetConfigurationData() {
        List<CustomerConfigurationData> customers = customerController.GetData().ConvertAll((x) => x as CustomerConfigurationData);
        Debug.Log($"\nCustomers:\n");
        customers.ForEach(x => Debug.Log(x.ToString() + ", "));
        List<ActionConfigurationData> actions = actionController.GetData().ConvertAll((x) => x as ActionConfigurationData);
        Debug.Log($"\nActions:\n{actions.ToString()}");
        actions.ForEach(x => Debug.Log(x.ToString() + ",\n"));
        Debug.Log("End of Actions");
        SimulationConfigurationData simulation = new SimulationConfigurationData(nameInput.text, int.Parse(startTime.text), int.Parse(endTime.text), int.Parse(numOfCustomers.text));
        Debug.Log($"\nSimulation:\n {simulation.ToString()}");
        Configurations configurations = new Configurations(simulation, customers, actions);
        //CreationManager.CONFIGURATION = configurations;
        Debug.Log($"\nConfigurations: {configurations.ToString()}");
        return configurations;
    }


}
