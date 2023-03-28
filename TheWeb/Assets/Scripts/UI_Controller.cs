using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    public Button start_button;
    public Button credit_button;
    public Button exit_button;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        start_button = root.Q<Button>("start_button");
        credit_button = root.Q<Button>("credit_button");
        exit_button = root.Q<Button>("exit_button");

        start_button.clicked += StartButtonPressed;
        credit_button.clicked += CreditButtonPressed;
        exit_button.clicked += ExitButtonPressed;
    }

    // Update is called once per frame
    void StartButtonPressed()
    {
        SceneManager.LoadScene("Controls");
    }

    void CreditButtonPressed() {
        SceneManager.LoadScene("Credits");
    }

    void ExitButtonPressed() {
        Application.Quit();
    }
}