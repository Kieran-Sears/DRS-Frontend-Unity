using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class ConfigurationController : FormCaller {

    public override void AddItem() {
        CreationManager.Instance.CreateConfigurationItem(this);
    }

    public override ConfigurationDelegateHolder SetFormDelegates(ConfigurationDelegateHolder delegates) {
        delegates.submissionDelegate += Main.Instance.LoadTrainUI;
        delegates.submissionDelegate += x => CreationManager.CONFIGURATION = x as Configurations;
        return delegates;
    }

}
