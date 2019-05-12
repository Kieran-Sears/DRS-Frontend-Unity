using System;
using System.Collections.Generic;
using static Enums;

public abstract class ConfigurationData {
    public string id;
}

public abstract class Value : ConfigurationData { } 

public class Configurations : ConfigurationData {

    public SimulationConfigurationData simulationConfiguration;
    public List<CustomerConfigurationData> customerConfigurations;
    public List<ActionConfigurationData> actionConfigurations;

    public Configurations(SimulationConfigurationData simulationConfiguration, List<CustomerConfigurationData> customerConfigurations, List<ActionConfigurationData> actionConfigurations) {
        this.id = Guid.NewGuid().ToString();
        this.simulationConfiguration = simulationConfiguration;
        this.customerConfigurations = customerConfigurations;
        this.actionConfigurations = actionConfigurations;
    }
  
    public override string ToString() {
        string cusConfigs = "";
        foreach (CustomerConfigurationData cus in customerConfigurations) {
            cusConfigs += $"{cus.ToString()}\n";
        }
        string actConfigs = "";
        foreach (ActionConfigurationData act in actionConfigurations) {
            actConfigs += $"{act.ToString()}\n";
        }
        return $"Configurations:\n" +
            $"    label:\n  {id}\n" +
            $"    simulation configuration:\n   {simulationConfiguration.ToString()}\n" +
            $"    customer configurations:\n    {cusConfigs}\n" +
            $"    action configurations:\n      {actConfigs}\n";
    }

   
}

public class SimulationConfigurationData : ConfigurationData {

    public int startTime;
    public int endTime;
    public int numberOfCustomers;

    public SimulationConfigurationData(string label, int startTime, int endTime, int numberOfCustomers) {
        this.id = label;
        this.startTime = startTime;
        this.endTime = endTime;
        this.numberOfCustomers = numberOfCustomers;
    }

    public override string ToString() {
        return $"Simulation Configuration:\n    Label:  {id}\n  start time: {startTime}\n   end time: {endTime}\n   number of customers: {numberOfCustomers}\n";
    }

}

public class CustomerConfigurationData : ConfigurationData {

    public List<AttributeConfigurationData> attributeConfigurations;

    public CustomerConfigurationData(string label, List<AttributeConfigurationData> attributeConfigurations) {
        this.id = label;
        this.attributeConfigurations = attributeConfigurations;
    }
    public override string ToString() {
        string attConfigs = "";
        foreach (AttributeConfigurationData att in attributeConfigurations) {
            attConfigs += $"{att.ToString()}\n";
        }
        return $"CustomerConfiguration:\n   label:{id}\n   attributes:{attConfigs}";
    }
}

public class ActionConfigurationData : ConfigurationData {
    public ActionConfigurationData(string label) {
        this.id = label;
    }
}

public class AttributeConfigurationData : ConfigurationData {

    public Value value;

    public AttributeConfigurationData(string label, Value value) {
        this.id = label;
        this.value = value;
    }

    public override string ToString() {
        return $"AttributeConfiguration:\n  label: {id}\n    value: {value.ToString()}";
    }
}

public class ScalarValue : Value {
    public string kind;
    public double start;
    public double min;
    public double max;
    public VarianceType variance;

    public ScalarValue(double min, double max, VarianceType variance, double start = -1) {
        this.id = Guid.NewGuid().ToString();
        this.kind = "scalar";
        this.start = start;
        this.min = min;
        this.max = max;
        this.variance = variance;
    }

    public override string ToString() {
        return $"ScalarValue:\n start: {start}\n    min: {min}\n    max: {max}\n    variance: {variance}";
    }

}

public class CategoricalValue : Value {
    public string kind;
    public List<CategoricalOption> options;

    public CategoricalValue(List<CategoricalOption> options) {
        this.id = Guid.NewGuid().ToString();
        this.kind = "categorical";
        this.options = options;
    }
    public override string ToString() {
        string catConfigs = "";
        foreach (CategoricalOption cat in options) {
            catConfigs += $"{cat.ToString()}, ";
        }

        return $"CategoricalValue:\n    options: {catConfigs.ToString()}";
    }
}

public class CategoricalOption : ConfigurationData {

    public CategoricalOption(string label) {
        this.id = label;
    }

    public override string ToString() {
        return id;
    }
}
