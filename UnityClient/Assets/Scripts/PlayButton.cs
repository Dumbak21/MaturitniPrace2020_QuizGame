using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayButton : MonoBehaviour
{
    // Scene: Main

    public TMP_InputField Nick;


    public TMP_Text error;

    public Button Join;
    public Button Create;


    private bool? CreateButt = null;

    void Start()
    {
        Join.interactable = false;
        Create.interactable = false;

        error.gameObject.SetActive(false);

        Nick.onValueChanged.AddListener(delegate { Buttons(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (DataManager.NickAdded == true)
        {
            Debug.Log("Update: Added");
            DataManager.NickName = Nick.text;

            if (CreateButt == true)
            {
                CreateButt = null;
                DataManager.LoadScene("CreateGame");
            }
            else if(CreateButt == false)
            {
                CreateButt = null;
                DataManager.LoadScene("JoinGame");
            }


        }
        else
        {
            if (DataManager.PlayerError != error.text)
            {
                Debug.Log("Update: New Error");
                error.text = DataManager.PlayerError;
                error.gameObject.SetActive(true);
            }
        }
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

        ServerManager.AddPlayer(Nick.text);
        CreateButt = false;
        //send Nickname to the server and ask if it exists
        //if it does show error mesage
        Debug.Log(Nick.text);
        //if (exists)
        //{
        //    Error.enabled = true;
        //}
        //else
        //{
        //    Error.enabled = false;
        //    DataManager.NickName = Nick.text;
        //    DataManager.LoadScene("JoinGame");
        //}

    }
    public void LoadCraeteGame()
    {
        ServerManager.AddPlayer(Nick.text);
        CreateButt = true;

        Debug.Log(Nick.text);

    }

}
