using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class ConfigurationController : FormCaller {

    public override void AddItem() {
        CreationManager.Instance.CreateConfigurationItem(this);
    }

    public override DelegateHolder SetFormDelegates(DelegateHolder delegates) {
        delegates.submissionDelegate += Main.Instance.LoadTrainUI;
        return delegates;
    }

}
