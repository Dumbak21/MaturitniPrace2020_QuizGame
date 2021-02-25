using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinGame : MonoBehaviour
{
    public TMP_InputField inputField;
    [SerializeField]
    public TMP_Text error;
    public Button join;
    // Start is called before the first frame update
    void Start()
    {
        join.interactable = false;
        error.gameObject.SetActive(false);
        inputField.onValueChanged.AddListener(delegate { Button(); });
    }

    public bool Joining { get; set; }

    private void Button()
    {
        Code = inputField.text;
        if(Code.Length > 3 && Code.Length < 5)
        {
            join.interactable = true;
        }
        else
        {
            join.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(DataManager.JoinedToRoom == true)
        {
            Debug.Log("Update: Joined");
            Joining = false;
            DataManager.LoadScene("Lobby");
        }
        else
        {
            if (DataManager.RoomError != error.text)
            {
                Debug.Log("Update: New Error");
                Joining = false;
                error.text = DataManager.RoomError;
                error.gameObject.SetActive(true);
            }
        }
    }

    //public bool exists = true;
    //public bool joinable = true;

    private string code;
    public string Code { get { return code; } set { code = value; } }

    public void Join()
    {

        //Try to join to *CODE*
        //JoinToServer(Code)

        Int32.TryParse(Code, out int intCode);
        Debug.Log(intCode);

        ServerManager.JoinToRoom(intCode);
        Joining = true;
        error.gameObject.SetActive(false);


    }


    public void Back()
    {

        DataManager.NickAdded = false;
        DataManager.PlayerError = "";
        ServerManager.RemovePlayer();
        DataManager.LoadPreviousScene();
    }

    //private Response JoinTOServer(string Code)
    //{
    //    //join to server
    //}
}
