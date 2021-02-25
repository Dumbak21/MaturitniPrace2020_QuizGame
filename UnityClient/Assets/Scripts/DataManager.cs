using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DataManager
{
    #region Main

    public static string NickName = "";
    public static string RoomCode = "";

    public static bool JoinedToRoom = false;
    public static bool NickAdded = false;
    public static string RoomError;
    public static string PlayerError;

    public static void RoomResponse(Packets packet)
    {
        switch (packet)
        {
            case Packets.S_RoomNotFound:
                {
                    RoomError = "This code does not exist :(";
                    JoinedToRoom = false;
                    break;
                }
            case Packets.S_RoomFull:
                {
                    RoomError = "The room you're trying to join is full";
                    JoinedToRoom = false;
                    break;
                }
            case Packets.S_PlayerNotFound:
                {
                    //I dunno how
                    RoomError = "You don't exist ?:)";
                    JoinedToRoom = false;
                    break;
                }
            case Packets.S_AddedToRoom:
                {
                    RoomError = "";
                    JoinedToRoom = true;
                    break;
                }
            case Packets.S_CreatedRoom:
                {
                    RoomError = "";
                    JoinedToRoom = true;
                    break;
                }
        }
    }

    public static void NickResponse(Packets packet)
    {
        switch (packet)
        {
            case Packets.S_PlayerExists:
                {
                    PlayerError = "This player exists :(";
                    NickAdded = false;
                    break;
                }
            case Packets.S_AddedPlayer:
                {
                    PlayerError = "";
                    NickAdded = true;
                    break;
                }
            
        }
    }
    #endregion

    #region SCENES
    public static string SceneToLoad;
    public static List<string> LastLoaded = new List<string>();

    public static void LoadScene(string scene)
    {
        SceneToLoad = scene;
        LastLoaded.Add(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Loading");
    }

    public static void LoadPreviousScene()
    {
        //LastLoaded.ForEach(x => Debug.Log(x.ToString()));
        if (LastLoaded.Count > 0)
        {
            SceneToLoad = LastLoaded[LastLoaded.Count - 1];
            LastLoaded.RemoveAt(LastLoaded.Count - 1);
            SceneManager.LoadScene("Loading");
        }

    }
    #endregion

    #region SETTINGS

    public static float Volume = 50;
    public static int Quality = 1;
    //public static Resolution resolution;


    #endregion

    #region COLORS  
    public static Color Green = new Color(0, 200 , 0);
    public static Color Idle = new Color(255, 112 , 112);
    #endregion
}


//In case of standalone app
public class Resolution
{
    public int[] res{ 
        get
        {
            return new int[]{ x, y };
        }
        set
        {
            x = value[0];
            y = value[1];
        }
    }

    private int x;
    private int y;
}