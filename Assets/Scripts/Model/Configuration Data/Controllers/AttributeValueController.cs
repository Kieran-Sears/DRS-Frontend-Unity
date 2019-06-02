using UnityEngine;
using UnityEngine.UI;


public class AttributeValueController : FormCaller {

    public AttributeHolder holder;
    
    public Button button;
 
    public void Start() {
        button.onClick.AddListener(() => CreationManager.Instance.CreateConfigurationItem(this, holder));
    }

    public override CreationForm SetFormDelegates(CreationForm form, ValueHolder holder) {
        this.holder = holder as AttributeHolder;
        form.submissionDelegate += ((x) => {
            this.holder.value = x as Value;
            form.ClearFields();
            form.gameObject.SetActive(false);
        });
        return form;
    }

   

    
}
