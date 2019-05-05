using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyFilter : MonoBehaviour {
    private InputField mainInputField;

    public void Start() {
        mainInputField = gameObject.GetComponent<InputField>();
        // Sets the MyValidate method to invoke after the input field's default input validation invoke (default validation happens every time a character is entered into the text field.)
        mainInputField.onValidateInput += delegate (string input, int charIndex, char addedChar) { return MyValidate(addedChar); };
    }

    private char MyValidate(char charToValidate) {
        //Checks if a dollar sign is entered....
        if (charToValidate == '£') {
            // ... if it is change it to an empty character.
            charToValidate = '\0';
        }
        return charToValidate;
    }
}
