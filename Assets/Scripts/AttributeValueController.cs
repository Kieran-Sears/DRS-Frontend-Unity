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
        form.submissionDelegate += ((x) => {form.gameObject.SetActive(false);});
        Debug.Log($"attributeValueHolder: {valueHolder}");
        Debug.Log($"attributeValueHolders value: {valueHolder.value}");
        form.submissionDelegate += ((v) => valueHolder.value = v as Value);
        return form;
    }

   

    
}
