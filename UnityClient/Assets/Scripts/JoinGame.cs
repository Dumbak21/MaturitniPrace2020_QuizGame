using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinGame : MonoBehaviour
{
    public TMP_InputField inputField;
    [SerializeField]
    private TMP_Text error;
    public Button join;
    // Start is called before the first frame update
    void Start()
    {
        join.interactable = false;
        error.gameObject.SetActive(false);
        inputField.onValueChanged.AddListener(delegate { Button(); });
    }

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
        
    }

    bool exists = true;
    bool joinable = true;

    private string code;
    public string Code { get { return code; } set { code = value; } }
    public void Join()
    {
        //Try to join to *CODE*
        //JoinToServer(Code)


        if (!exists)
        {
            error.text = "This code does not exist :(";
            error.gameObject.SetActive(true);
        }
        else if (!joinable)
        {
            error.text = "Someone of us is experiencing network issues, please try again later";
            error.gameObject.SetActive(true);
        }
        error.gameObject.SetActive(false);

        DataManager.LoadScene("Game_MAIN");
    }

    //private Response JoinTOServer(string Code)
    //{
    //    //join to server
    //}
}
