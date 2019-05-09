using System;
using System.Xml.Serialization;
using System.Collections.Generic;


public abstract class ConfigurationData {
    public abstract string GetLabel();
}
public abstract class Value : ConfigurationData { } 

[XmlRoot("Configuration")]
[Serializable]
public class Configurations : ConfigurationData {

    [XmlAttribute("label")]
    private string label;

    [XmlAttribute("ID")]
    private string ID { get; set; }
  
    [XmlElement("SimulationConfiguration")]
    private float simulationConfiguration { get; set; }

    [XmlArray("CustomerConfigurations")]
    [XmlArrayItem("CustomerConfiguration")]
    private List<CustomerConfigurationData> customerConfigurations = new List<CustomerConfigurationData>();

    [XmlArray("ActionConfigurations")]
    [XmlArrayItem("ActionConfiguration")]
    private List<ActionConfigurationData> actionConfigurations = new List<ActionConfigurationData>();

    public Configurations(string label, float simulationConfiguration, List<CustomerConfigurationData> customerConfigurations, List<ActionConfigurationData> actionConfigurations) {
        this.label = label;
        this.ID = Guid.NewGuid().ToString();
        this.simulationConfiguration = simulationConfiguration;
        this.customerConfigurations = customerConfigurations;
        this.actionConfigurations = actionConfigurations;
    }

    public List<CustomerConfigurationData> GetCustomerConfigs() {
        return customerConfigurations;
    }

    public override string GetLabel() {
        return label;
    }
}

[Serializable]
public class CustomerConfigurationData : ConfigurationData {

    [XmlAttribute("label")]
    private string label;

    [XmlArray("AttributeConfigurations")]
    [XmlArrayItem("AttributeConfiguration")]
    private List<AttributeConfigurationData> attributeConfigurations;

    public CustomerConfigurationData(string label, List<AttributeConfigurationData> attributeConfigurations) {
        this.label = label;
        this.attributeConfigurations = attributeConfigurations;
    }

    public override string GetLabel() {
        return label;
    }

    public List<AttributeConfigurationData> GetAttributeConfigurations() {
        return attributeConfigurations;
    }
}

[Serializable]
public class ActionConfigurationData : ConfigurationData {
    [XmlAttribute("label")]
    private string label;

    public ActionConfigurationData(string label) {
        this.label = label;
    }

    public override string GetLabel() {
        return label;
    }
}


[Serializable]
public class AttributeConfigurationData : ConfigurationData {

    [XmlAttribute("label")]
    private string label;

    [XmlAttribute("value")]
    private Value value;

    public AttributeConfigurationData(string name, Value value) {
        this.label = name;
        this.value = value;
    }

    public override string GetLabel() {
        return label;
    }

    public Value GetValue() {
        return value;
    }
}


//case class Configurations(
//  customer: List[CustomerConfig] = Nil,
//  simulation: SimulationConfig = SimulationConfig()
//)
//
//case class CustomerConfig(
//  name: String = "Customer" + UUID.randomUUID(),
//  arrears: Scalar,
//  attributes: List[Attribute] = Nil,
//  proportion: Int = 100,
//  appearance: Variance.Value = Variance.None,
//  assignedLabel: Int,
//  kind: String = "customer")
//
//case class SimulationConfig(
//  startState: Option[State] = None,
//  startTime: Int = 0,
//  endTime: Option[Int] = None,
//  debtVarianceOverTime: Variance.Value = Variance.None,
//  numberOfCustomers: Int = 10,
//  kind: String = "simulation")
