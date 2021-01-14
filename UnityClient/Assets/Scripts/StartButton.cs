using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //var gm = FindObjectOfType<GameManager>();
        //gm.a = "a";
    }

    public void Back()
    {
        DataManager.LoadPreviousScene();
    }

    public void PlaySound()
    {
        var source = Component.FindObjectOfType<AudioSource>();
        source.PlayOneShot(source.clip);
    }

    public void LoadMenu()
    {
        DataManager.LoadScene("Settings");
    }

    public void LoadGame()
    {
        DataManager.LoadScene("Main");

    }
}
