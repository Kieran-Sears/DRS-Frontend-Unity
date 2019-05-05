using System;
using System.Xml.Serialization;
using System.Collections.Generic;

public abstract class Value { }
public abstract class ConfigurationData { }

[XmlRoot("Configuration")]
[Serializable]
public class Configurations : ConfigurationData {

    [XmlAttribute("name")]
    public string name;

    [XmlAttribute("ID")]
    public int ID { get; set; }
  
    [XmlElement("SimulationConfiguration")]
    public float simulationConfiguration { get; set; }

    [XmlArray("CustomerConfigurations")]
    [XmlArrayItem("CustomerConfiguration")]
    public List<CustomerConfigurationData> CustomerConfigurations = new List<CustomerConfigurationData>();

    [XmlArray("ActionConfigurations")]
    [XmlArrayItem("ActionConfiguration")]
    public List<ActionConfigurationData> actionConfigurations = new List<ActionConfigurationData>();

}

[Serializable]
public class CustomerConfigurationData : ConfigurationData {

    [XmlAttribute("averageArrears")]
    public string averageArrears;

    [XmlAttribute("minRangeArrears")]
    public string minRangeArrears;

    [XmlAttribute("maxRangeArrears")]
    public string maxRangeArrears;

    [XmlAttribute("variationOnArrears")]
    public string variationOnArrears;

    [XmlArray("AttributeConfigurations")]
    [XmlArrayItem("AttributeConfiguration")]
    public AttributeConfigurationData[] attributeConfigurations;

    public CustomerConfigurationData(string averageArrears, string minRangeArrears, string maxRangeArrears, string variationOnArrears, AttributeConfigurationData[] attributeConfigurations) {
        this.averageArrears = averageArrears;
        this.minRangeArrears = minRangeArrears;
        this.maxRangeArrears = maxRangeArrears;
        this.variationOnArrears = variationOnArrears;
        this.attributeConfigurations = attributeConfigurations;
    }
}

[Serializable]
public class ActionConfigurationData : ConfigurationData {

}


[Serializable]
public class AttributeConfigurationData : ConfigurationData {

    [XmlAttribute("name")]
    public string name;

    [XmlAttribute("value")]
    public Value value;

    public AttributeConfigurationData(string name, Value value) {
        this.name = name;
        this.value = value;
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
