using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationValueController : FormCaller
{
    public ConfigurationHolder valueHolder;

    public Button button;

    public void Start() {
        button.onClick.AddListener(() => CreationManager.Instance.CreateConfigurationItem(this, valueHolder));
    }

    public override CreationForm SetFormDelegates(CreationForm form, ValueHolder holder) {
        valueHolder = holder as ConfigurationHolder;
        form.submissionDelegate += ((x) => { form.gameObject.SetActive(false); });
        form.submissionDelegate += ((c) => valueHolder.value = c as Configurations);
        form.submissionDelegate += ((c) => {
            StartCoroutine(NetworkManager.Instance.SendConfigurationRequest(valueHolder.value));
        });
        return form;
    }


}
