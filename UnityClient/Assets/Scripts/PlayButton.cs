using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayButton : MonoBehaviour
{
    // Scene: Main

    public TMP_InputField Nick;


    public TMP_Text Error;

    public Button Join;
    public Button Create;


    public bool exists = false;

    void Start()
    {
        Join.interactable = false;
        Create.interactable = false;

        Error.enabled = false;

        Nick.onValueChanged.AddListener(delegate { Buttons(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buttons()
    {
        if(Nick.text.Length > 0)
        {
            Join.interactable = true;
            Create.interactable = true;
        }
        else
        {
            Join.interactable = false;
            Create.interactable = false;
        }
    }


    public void Back()
    {
        DataManager.LoadPreviousScene();
    }

    public void LoadJoinGame()
    {
        //send Nickname to the server and ask of it exists
        //if it does show error mesage
        Debug.Log(Nick.text);
        Debug.Log(exists);
        if (exists)
        {
            Error.enabled = true;
        }
        else
        {
            Error.enabled = false;
            DataManager.NickName = Nick.text;
            DataManager.LoadScene("JoinGame");
        }

    }
    public void LoadCraeteGame()
    {
        if (exists)
        {
            Error.enabled = true;
        }
        else
        {
            Error.enabled = false;
            DataManager.NickName = Nick.text;
            DataManager.LoadScene("CreateGame");
        }

    }

}
