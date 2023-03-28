using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Back_Controller : MonoBehaviour
{
    public Button back_button;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        back_button = root.Q<Button>("back_button");

        back_button.clicked += BackButtonPressed;
    }

    void BackButtonPressed() {
        SceneManager.LoadScene("MainMenu");
    }
}
