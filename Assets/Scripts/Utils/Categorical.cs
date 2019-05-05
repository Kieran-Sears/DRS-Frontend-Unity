using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Categorical : MonoBehaviour
{

    public Button submitButton;

    public delegate void SubmitCategoricalDelegate(CategoricalValue val);
    public SubmitCategoricalDelegate nameChangeDelegate;

    public void Display(SubmitCategoricalDelegate del) {
        List<string> x = new List<string>();
        x.Add("NotImplemented");
        submitButton.onClick.AddListener(() => del(new CategoricalValue(x)));
    }

}

public class CategoricalValue : Value {

    public List<string> name;

    public CategoricalValue(List<string> name) {
        this.name = name;
    }
    public override string ToString() {
        return $"start: {name}";
    }

}
