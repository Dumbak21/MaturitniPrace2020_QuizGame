using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class Settings : MonoBehaviour
{

    public TMP_Dropdown dropdown;
    public Slider slider;
    public AudioMixer audioMixer;

    public TMP_Text text;


    //public GameManager manager;
    //manager = GameObject.FindObjectOfType<GameManager>();


    void Start()
    {
        text.enabled = false;
        //import from data
        slider.value = DataManager.Volume;
        slider.onValueChanged.AddListener(delegate { ChangeVolume(slider.value); } );

        dropdown.value = DataManager.Quality;
        dropdown.onValueChanged.AddListener(delegate { ChangeQuality(dropdown.value); } );
    }
    private void ChangeVolume(float vol)
    {
        text.enabled = false;
        if (vol <= 40f)
        {
            audioMixer.SetFloat("MainVolume", -80);
        }
        else
        {
            audioMixer.SetFloat("MainVolume", vol - 80);
        }
    }

    private void ChangeQuality(int chosen)
    {
        text.enabled = false;
        QualitySettings.SetQualityLevel(chosen);
    }


    public void Save()
    {
        DataManager.Volume = slider.value;
        DataManager.Quality = dropdown.value;
        text.enabled = true;
    }

    public void Back()
    {
        ChangeVolume(DataManager.Volume);
        ChangeQuality(DataManager.Quality);
        DataManager.LoadPreviousScene();
    }

    void Update()
    {
        
    }
}
