using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioHandler : MonoBehaviour
{

    public AudioMixer mixer;
    public static float volumeLevel = 1f;
    private Slider sliderVolumeCtrl;

    // Start is called before the first frame update
    void Start()
    {
        SetLevel(volumeLevel);
        GameObject sliderTemp = GameObject.FindWithTag("VolumeSlider");
        if (sliderTemp != null){
            sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
            sliderVolumeCtrl.value = volumeLevel;
        }
    }

    public void SetLevel (float sliderValue){
        mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
        volumeLevel = sliderValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
