using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMessage : MonoBehaviour
{
    public Text errorText;
    public Button okButton;

    void Start()
    {
        okButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    public void DisplayError(string error) {
        errorText.text = error;
        gameObject.SetActive(true);
    }

}
