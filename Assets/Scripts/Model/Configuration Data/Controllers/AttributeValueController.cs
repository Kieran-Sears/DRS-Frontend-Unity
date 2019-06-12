using UnityEngine;
using UnityEngine.UI;


public class AttributeValueController : FormCaller {

    public Value value;
    public Text display;

    public override void AddItem() {
        CreationManager.Instance.CreateConfigurationItem(this);
    }

    public void AddItem(ConfigurationData data) {
        throw new System.NotImplementedException();
    }

    public override ConfigurationDelegateHolder SetFormDelegates(ConfigurationDelegateHolder delegates) {
        delegates.submissionDelegate += SetValue;
        return delegates;
    }

    private void SetValue(ConfigurationData val) {
        Value value = val as Value;
        this.value = value;
        display.text = value.ToString();
    }
}
