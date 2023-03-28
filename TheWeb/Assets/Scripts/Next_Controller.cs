using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Next_Controller : MonoBehaviour
{
    public Button next_button;
    public Button back_button;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        next_button = root.Q<Button>("next_button");
        back_button = root.Q<Button>("back_button");

        next_button.clicked += NextButtonPressed;
        back_button.clicked += BackButtonPressed;
    }

    // Update is called once per frame
    void NextButtonPressed()
    {
        SceneManager.LoadScene("FinishedPlay");
    }

    void BackButtonPressed() {
        SceneManager.LoadScene("MainMenu");
    }
}