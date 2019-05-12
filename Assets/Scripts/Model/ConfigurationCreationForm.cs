using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationCreationForm : CreationForm
{
    public InputField nameInput;

    public InputField startTime;
    public InputField endTime;
    public InputField numOfCustomers;

    public ItemListController customerItemList;
    public ItemListController actionItemList;

    public Button submitButton;

    public void Start() {
        submitButton.onClick.AddListener(() => Submit());
    }

    public override void ClearFields() {
        customerItemList.ClearItems();
        actionItemList.ClearItems();
        nameInput.text = "";
    }

    public override void Prepopulate(ConfigurationData data) {
        Debug.Log($"Prepopulating Configuration data:\n{data.ToString()}");
        Configurations configurationData = data as Configurations;
        nameInput.text = configurationData.id;
        List<CustomerConfigurationData> customers = configurationData.customerConfigurations;
        foreach (CustomerConfigurationData customer in customers) {
            customerItemList.AddItem();
        }
        List<ActionConfigurationData> actions = configurationData.actionConfigurations;
        foreach (ActionConfigurationData action in actions) {
            actionItemList.AddItem(action);
        } 
    }

    private void Submit() {
        string validation = Validate();
        if (validation == "OK") {
            Configurations c = GetCurrentValues() as Configurations;
           submissionDelegate(c);
        } else {
            errorMessage.DisplayError(validation);
        };
    }

    private string Validate() {
        string ret = "OK";
        ret = string.IsNullOrEmpty(nameInput.text) ? "Please assign a Configuration Name" : ret;
        ret = string.IsNullOrEmpty(startTime.text) ? "Please assign a Configuration Name" : ret;
        ret = string.IsNullOrEmpty(endTime.text) ? "Please assign a Configuration Name" : ret;
        ret = string.IsNullOrEmpty(numOfCustomers.text) ? "Please assign a Configuration Name" : ret;
        return ret;
    }

    private ConfigurationData GetCurrentValues() {
        List<CustomerConfigurationData> customers = customerItemList.GetData().ConvertAll((x) => x as CustomerConfigurationData);
        List<ActionConfigurationData> actions = customerItemList.GetData().ConvertAll((x) => x as ActionConfigurationData);
        SimulationConfigurationData simulation = new SimulationConfigurationData(nameInput.text, int.Parse(startTime.text), int.Parse(endTime.text), int.Parse(numOfCustomers.text));
        return new Configurations(simulation, customers, actions);
    }


}
