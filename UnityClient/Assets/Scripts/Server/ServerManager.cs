using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    void Start()
    {
        NetworkDataHandler.InitPackets();
        Client.Connect();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        Client.AppStop();
    }
}
