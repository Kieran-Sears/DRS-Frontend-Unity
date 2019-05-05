using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AttributeCreationForm : MonoBehaviour
{
    public InputField nameInput;
    public Value value;
    public GameObject scalarPrefab;
    public GameObject categoricalPrefab;
    public Text summaryTextbox;
    public Button submitButton;

    public delegate void SubmitAttributeDelegate(AttributeConfigurationData data);
    public SubmitAttributeDelegate submitAttributeDelegate;

    public delegate void NameChangeDelegate(string name);
    public NameChangeDelegate nameChangeDelegate;

    public void Display(ListItemLabel nameLabel) {
        gameObject.SetActive(true); 
        nameChangeDelegate += nameLabel.UpdateText;
        nameInput.onEndEdit.AddListener((string newName) => nameChangeDelegate(newName));

        submitAttributeDelegate += nameLabel.SaveData;
        submitAttributeDelegate += (x) => {
            gameObject.SetActive(false);
            Debug.Log("Attribute Creation Complete, Saving Data");
        };
        submitButton.onClick.AddListener(() => submitAttributeDelegate(new AttributeConfigurationData(nameInput.text, value)));
    }

    public void CreateScalar() {
        GameObject scalarGO = Instantiate(scalarPrefab, transform);
        ScalarCreationForm scalar = scalarGO.GetComponent<ScalarCreationForm>();
        scalar.Display(DisplayValueSummary);
    }

    public void CreateCategorical() {
        GameObject categoricalGO = Instantiate(categoricalPrefab, transform);
        Categorical categorical = categoricalGO.GetComponent<Categorical>();
        categorical.Display(DisplayValueSummary);
    }

    public void DisplayValueSummary(Value value) {
        summaryTextbox.text = value.ToString();
    }
}
