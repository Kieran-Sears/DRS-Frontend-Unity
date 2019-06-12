using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class TrainingManager : MonoBehaviour {

    public ListController customers;
    public ListController attributes;
    public ListController actions;
    public MapController  effects;

    public EffectTrainingForm effectForm;
    public ErrorMessage errorMessage;

    public void Start() {
        ResetDelegates();
    }

    private void ResetDelegates() {
        customers.delegates.ClearAllDelegates();
        attributes.delegates.ClearAllDelegates();
        actions.delegates.ClearAllDelegates();
        effects.delegates.ClearAllDelegates();

        customers.delegates.selectionDelegate += InitAttributes;
        actions.delegates.selectionDelegate += InitEffects;
        effects.delegates.selectionDelegate += LoadEffectForm;
        effects.delegates.validationDelegate += EffectValidation;
        effects.delegates.submissionDelegate += EffectSubmission;
    }

    public void Init(Configurations config, TrainingData results) {
        customers.AddItems(results.customers.ConvertAll(x => x as TrainingItem));
        actions.AddItems(results.actions.ConvertAll(x => x as TrainingItem));
    }

    public void InitAttributes(TrainingItem trainingItem) {
        Customer customer = trainingItem as Customer;
        attributes.ClearItems();
        attributes.AddItems(customer.featureValues.ConvertAll(x => x as TrainingItem));
        if (actions.currentlySelected != null) {
            actions.delegates.selectionDelegate(actions.currentlySelected as TrainingItem);
        }
    }

    public void InitEffects(TrainingItem trainingItem) {
        Action action = trainingItem as Action;
        ActionTrainingView actionView = action.view as ActionTrainingView;

        effects.ClearItems();
        effects.AddItems(action.effects.ConvertAll(x => x as TrainingItem));


        List<(AttributeTrainingView, ActionTrainingView, EffectType)> attLinks = action.effects.ConvertAll(effect => {
            AttributeTrainingView attView = attributes.itemList.Find(attribute => attribute.name.text == effect.target) as AttributeTrainingView;
            return (attView, actionView, effect.type);
        });

        effects.DrawLines(attLinks);
    }

    public void LoadEffectForm(TrainingItem item) {
        Effect effect = item as Effect;
        AttributeTrainingView attView = attributes.itemList.Find(att => att.name.text == effect.target) as AttributeTrainingView;
        effectForm.Prepopulate(effect, attView.value.text);
        effectForm.submit.onClick.AddListener(() => effects.delegates.submissionDelegate(effect));
        effectForm.gameObject.SetActive(true);
    }

    public string EffectValidation(TrainingItem item) {
        Effect effect = item as Effect;
        string ret = "OK";
        ret = Utilities.NumberValidation(effect.value)  ? ret : "Please assign a value"; 
        ret = Utilities.NumberValidation(effect.deviation) ? ret : "Please assign a deviation";
        return ret;
    }

    public void EffectSubmission(TrainingItem item) {
        Effect effect = item as Effect;
        string validation = effects.delegates.validationDelegate(effect);
        if (validation == "OK") {
            Debug.Log($"Submit added:\n     {effectForm.gameObject.name}\n\nCreationConfiguration:\n    {effect}");
            effect.value = double.Parse(effectForm.value.text);
            effect.deviation = double.Parse(effectForm.deviation.text);
            EffectTrainingView view = effect.view as EffectTrainingView;
            view.lineRenderer.material = view.lineRenderer.materials[1];
            effectForm.gameObject.SetActive(false);
            effects.delegates.submissionDelegate = null;
            effectForm.submit.onClick.RemoveAllListeners();
            effectForm.ClearFields();
        } else {
            errorMessage.DisplayError(validation);
        };
    }


}

public class TrainingDelegateHolder {
    public delegate void CancellationDelegate();
    public CancellationDelegate cancelationDelegate = null;

    public delegate void SelectionDelegate(TrainingItem item);
    public SelectionDelegate selectionDelegate = null;

    public delegate void SubmissionDelegate(TrainingItem item);
    public SubmissionDelegate submissionDelegate = null;

    public delegate string ValidatationDelegate(TrainingItem val);
    public ValidatationDelegate validationDelegate = null;

    public void ClearAllDelegates() {
        cancelationDelegate = null;
        selectionDelegate = null;
        submissionDelegate = null;
        validationDelegate = null;
    }
}

public abstract class TrainingController : MonoBehaviour {

    public TrainingDelegateHolder delegates = new TrainingDelegateHolder();

    public RectTransform prefab;

    public List<TrainingItemView> itemList = new List<TrainingItemView>();

    public TrainingItem currentlySelected;

    public abstract void AddItems(List<TrainingItem> data);

    public abstract void ClearItems();
}