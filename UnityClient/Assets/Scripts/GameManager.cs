using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    private static GameManager _ins;
    public static GameManager Instance { get { return _ins; } set { _ins = value; } }

    void Start()
    {
        audioMixer.SetFloat("MainVolume", (DataManager.Volume -80));
    }

    void Awake()
    {


        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

}
