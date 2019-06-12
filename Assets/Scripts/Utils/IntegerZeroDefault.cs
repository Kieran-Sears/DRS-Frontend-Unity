using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntegerZeroDefault : MonoBehaviour {
    void Start() {
        InputField field = GetComponent<InputField>();
        field.text = "0";
        field.onEndEdit.AddListener(x => field.text = CheckForNull(x));
    }

    private string CheckForNull(string text) {
       return string.IsNullOrEmpty(text) ? "0" : text;
    }

}
