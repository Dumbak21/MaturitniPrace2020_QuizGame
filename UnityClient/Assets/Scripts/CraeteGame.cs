using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraeteGame : MonoBehaviour
{

    public TMP_Text error;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Create()
    {
        ServerManager.CreateRoom();
    }

    // Update is called once per frame
    void Update()
    {
        if (DataManager.JoinedToRoom == true)
        {
            Debug.Log("Update: Created");
            DataManager.LoadScene("Lobby");
        }
        else
        {
            if (DataManager.RoomError != error.text)
            {
                Debug.Log("Update: New Error");
                error.text = DataManager.RoomError;
                error.gameObject.SetActive(true);
            }
        }
    }
}
