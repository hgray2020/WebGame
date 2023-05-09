using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{

    public AudioMixer mixer;
    public static float volumeLevel = 1f;
    private Slider sliderVolumeCtrl;


    // Start is called before the first frame update
    void Start()
    {
         // Audio
        SetLevel(volumeLevel);
        GameObject sliderTemp = GameObject.FindWithTag("VolumeSlider");
        if (sliderTemp != null){
            sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
            sliderVolumeCtrl.value = volumeLevel;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel (float sliderValue){
        mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
        volumeLevel = sliderValue;
    }

    public void PlayScene() {
        SceneManager.LoadScene("Henry_Workspace_Scene");
    }

    public void QuitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
