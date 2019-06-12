using System;
using System.Collections.Generic;
using static Enums;

public abstract class ConfigurationData {
    public string id = Guid.NewGuid().ToString();
    public string name;
    public ConfigItemTypes kind;
}

public abstract class Value : ConfigurationData { } 

public class Configurations : ConfigurationData {

    public SimulationConfigurationData simulationConfiguration;
    public List<CustomerConfigurationData> customerConfigurations;
    public List<ActionConfigurationData> actionConfigurations;
    public List<EffectConfigurationData> effectConfigurations;
    public List<AttributeConfigurationData> attributeConfigurations;
    public List<CategoricalOptionConfigurationData> optionConfigurations;

    public Configurations(
        SimulationConfigurationData simulationConfiguration, 
        List<CustomerConfigurationData> customerConfigurations, 
        List<ActionConfigurationData> actionConfigurations, 
        List<EffectConfigurationData> effectConfigurations, 
        List<AttributeConfigurationData> attributeConfigurations, 
        List<CategoricalOptionConfigurationData> optionConfigurations) 
    {
        this.name = "ConfigurationData"; // TODO rename to match saved data when implementing data persistence
        this.kind = ConfigItemTypes.Configuration;
        this.simulationConfiguration = simulationConfiguration;
        this.customerConfigurations = customerConfigurations;
        this.actionConfigurations = actionConfigurations;
        this.effectConfigurations = effectConfigurations;
        this.attributeConfigurations = attributeConfigurations;
        this.optionConfigurations = optionConfigurations;
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
        string effConfigs = "";
        foreach (EffectConfigurationData eff in effectConfigurations) {
            effConfigs += $"{eff.ToString()}\n";
        }
        string attConfigs = "";
        foreach (AttributeConfigurationData att in attributeConfigurations) {
            attConfigs += $"{att.ToString()}\n";
        }
        string optConfigs = "";
        foreach (CategoricalOptionConfigurationData opt in optionConfigurations) {
            optConfigs += $"{opt.ToString()}\n";
        }
        return $"Configurations:\n" +
            $"    label: {name}\n" +
            $"    simulation configuration:\n       {simulationConfiguration.ToString()}\n" +
            $"    customer configurations:\n        {cusConfigs}\n" +
            $"    action configurations:\n          {actConfigs}\n" +
            $"    effect configurations:\n          {effConfigs}\n" +
            $"    attribute configurations:\n       {attConfigs}\n" +
            $"    option configurations:\n          {optConfigs}\n";
    }

   
}

public class SimulationConfigurationData : ConfigurationData {

    public int startTime;
    public int endTime;
    public int numberOfCustomers;

    public SimulationConfigurationData(string label, int startTime, int endTime, int numberOfCustomers) {
        this.name = label;
        this.kind = ConfigItemTypes.Simulation;
        this.startTime = startTime;
        this.endTime = endTime;
        this.numberOfCustomers = numberOfCustomers;
    }

    public override string ToString() {
        return $"Simulation Configuration:\n    Label: {name}\n    start time: {startTime}\n    end time: {endTime}\n    number of customers: {numberOfCustomers}\n";
    }

}

public class CustomerConfigurationData : ConfigurationData {
    
    public int proportion;
    public ScalarValue arrears;
    public ScalarValue satisfaction;
    public List<string> attributeConfigurations;

    public CustomerConfigurationData(string label, int proportion, ScalarValue arrears, ScalarValue satisfaction, List<string> attributeConfigurations) {
        this.name = label;
        this.kind = ConfigItemTypes.Customer;
        this.proportion = proportion;
        this.arrears = arrears;
        this.satisfaction = satisfaction;
        this.attributeConfigurations = attributeConfigurations;
    }
    public override string ToString() {
        string attConfigs = "";
        foreach (string att in attributeConfigurations) {
            attConfigs += $"\n          {att.ToString()}";
        }
        return $"CustomerConfiguration:\n   label:{name}\n   attributes:{attConfigs}";
    }
}



public class AttributeConfigurationData : ConfigurationData {
  
    public Value value;

    public AttributeConfigurationData(string label, Value value) {
        this.name = label;
        this.kind = ConfigItemTypes.Attribute;
        this.value = value;
    }

    public override string ToString() {
        return $"AttributeConfiguration:\n  label: {name}\n    value: {value.ToString()}";
    }
}

public class ScalarValue : Value {
    public int min;
    public int max;
    public VarianceType variance;

    public ScalarValue(int min, int max, VarianceType variance, double start = -1) {
        this.name = Guid.NewGuid().ToString();
        this.kind = ConfigItemTypes.Scalar;
        this.min = min;
        this.max = max;
        this.variance = variance;
    }

    public override string ToString() {
        return $"ranges: min: {min} / max: {max} / variance: {variance}";
    }

}

public class CategoricalValue : Value {
    public List<string> options;

    public CategoricalValue(List<string> options) {
        this.name = Guid.NewGuid().ToString();
        this.kind = ConfigItemTypes.Categorical;
        this.options = options;
    }
    public override string ToString() {
        string catConfigs = "";
        foreach (string op in options) {
            CategoricalOptionConfigurationData option = CreationManager.OPTIONS.Find(x => x.name == Utilities.UpperFirst(op));
            catConfigs += $"    {option.ToString()},\n";
        }

        return $"options:\n{catConfigs.ToString()}";
    }
}

public class CategoricalOptionConfigurationData : ConfigurationData {

    public int probability;

    public CategoricalOptionConfigurationData(string label, int probability) {
        this.name = label;
        this.kind = ConfigItemTypes.CategoricalOption;
        // TODO introduce probability to options
        if (probability == 0) {
            probability = UnityEngine.Random.Range(1, 100);
        }
        this.probability = probability;
    }

    public override string ToString() {
        return $"{{id: {name}, probability: {probability}}}";
    }
}

public class ActionConfigurationData : ConfigurationData {

    public ActionType type;
    public List<string> effectConfigurations;

    public ActionConfigurationData(
        string label,
        ActionType type,
        List<string> effectConfigurations) {
        this.name = label;
        this.kind = ConfigItemTypes.Action;
        this.type = type;
        this.effectConfigurations = effectConfigurations;
    }

    public override string ToString() {
        string effConfigs = "";
        foreach (string eff in effectConfigurations) {
            effConfigs += $"\n          {eff.ToString()}, ";
        }

        return $"Action:\n      Label: {name}\n       Type: {type}\n       Effects: {effConfigs.ToString()}";
    }
}

public class EffectConfigurationData : ConfigurationData {

    public EffectType type;
    public string target;

    public EffectConfigurationData(string label, EffectType type, string target) {
        this.name = label;
        this.kind = ConfigItemTypes.Effect;
        this.type = type;
        this.target = target;
    }
    public override string ToString() {
        return $"   label:{name}  type:{type}  target:{target}";
    }
}

