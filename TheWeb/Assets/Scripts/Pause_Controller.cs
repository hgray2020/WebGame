using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Pause_Controller : MonoBehaviour {

        public Button resume_button;
        public Button controls_button;
        public Button exit_button;

        private UIDocument pause_menu;

        public static bool GameisPaused = false;

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

                pause_menu = GetComponent<UIDocument>();
                pause_menu.enabled = !pause_menu.enabled;
                GameisPaused = false;
        }

        // Update is called once per frame
        void Update() {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                        if (GameisPaused) {
                                ResumeButtonPressed();
                        } else {
                                Pause();
                        }
                }
        }

        void Pause() {
                pause_menu.enabled = !pause_menu.enabled;
                Time.timeScale = 0f;
                GameisPaused = true;
        }
        void ResumeButtonPressed()
        {
                Time.timeScale = 1f;
                GameisPaused = false;
                pause_menu.enabled = !pause_menu.enabled;
        }

        void ControlsButtonPressed() {
                SceneManager.LoadScene("Controls");
        }

        void ExitButtonPressed() {
                SceneManager.LoadScene("MainMenu");
        }
}
