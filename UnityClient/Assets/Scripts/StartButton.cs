using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StartButton : MonoBehaviour
{

    public AudioSource audio;

    public AudioClip clip;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void PlaySound()
    {

        audio.PlayOneShot(clip);
    }

    public void LoadMenu()
    {
        
    }
}
