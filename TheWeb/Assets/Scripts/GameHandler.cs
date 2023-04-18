using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {


        public static bool GameisPaused = false;
        public GameObject pauseMenuUI;
        public GameObject antSpawn;

        void Start (){
                pauseMenuUI.SetActive(false);
                GameisPaused = false;
        }

        void Update (){
                if (Input.GetKeyDown(KeyCode.Escape)){
                        if (GameisPaused){
                                Resume();
                        }
                        else{
                                Pause();
                        }
                }
                if(Input.GetMouseButtonDown(0)) {
                        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector3 offset = new Vector3(0, 0, 10);
                        Instantiate(antSpawn, position + offset, Quaternion.identity);
                }
        }

        void Pause(){
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                GameisPaused = true;
        }

        public void Resume(){
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                GameisPaused = false;
        }

        public void SetPause(bool val) {
                GameisPaused = val;
        }
}
