using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationController : MonoBehaviour
{
    public Button backButton;
    public Text title;
    public string titleText = "Title Not Set...";

    public void Start() {
        title.text = titleText;
        backButton.onClick.AddListener(() => transform.parent.gameObject.SetActive(false));
    }

    

}
