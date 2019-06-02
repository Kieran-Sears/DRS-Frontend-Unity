using UnityEngine;
using UnityEngine.UI;


public class AttributeValueController : FormCaller {

    public AttributeHolder valueHolder;
    
    public Button button;
 
    public void Start() {
        button.onClick.AddListener(() => CreationManager.Instance.CreateConfigurationItem(this, valueHolder));
    }

    public override CreationForm SetFormDelegates(CreationForm form, ValueHolder holder) {
        valueHolder = holder as AttributeHolder;
        form.submissionDelegate += ((x) => {
            valueHolder.value = x as Value;
            form.ClearFields();
            form.gameObject.SetActive(false);
        });
        return form;
    }

   

    
}
