using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Pause_Controller : MonoBehaviour {

        public GameObject game_handler;
        
        public Button resume_button;
        public Button controls_button;
        public Button exit_button;

        // Start is called before the first frame update
        void Start()
        {

                var root = GetComponent<UIDocument>().rootVisualElement;

                resume_button = root.Q<Button>("resume_button");
                controls_button = root.Q<Button>("controls_button");
                exit_button = root.Q<Button>("exit_button");

                resume_button.clicked += ResumeButtonPressed;
                controls_button.clicked += ControlsButtonPressed;
                exit_button.clicked += ExitButtonPressed;
        }

        void ResumeButtonPressed()
        {
                game_handler.SendMessage("Resume");
        }

        void ControlsButtonPressed() {
                SceneManager.LoadScene("Controls");
        }

        void ExitButtonPressed() {
                Time.timeScale = 1f;
                game_handler.GetComponent<GameHandler>().SetPause(false);
                SceneManager.LoadScene("MainMenu");
        }


}
