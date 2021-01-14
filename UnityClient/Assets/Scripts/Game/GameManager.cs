using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    private static GameManager _ins;
    public static GameManager Instance { get { return _ins; } set { _ins = value; } }

    private static int currentPlayerIndex = 0;
    public static Player currentPlayer = null;
    public static Player[] players;

    private static GameObject PlayerList;

    public static void StartGame(Player[] pl, ref GameObject pls)
    {
        PlayerList = pls;
        players = pl;
        currentPlayer = players[currentPlayerIndex];
    }
    public static void NextPlayer()
    {
        currentPlayerIndex++;
        if(currentPlayerIndex >= players.Length) { currentPlayerIndex = 0; }
        currentPlayer = players[currentPlayerIndex];
        SetPlayerDesign();
    }
    public static void SetPlayerDesign()
    {
        for (int i = 0; i < PlayerList.gameObject.transform.childCount; i++)
        {
            PlayerList.gameObject.transform.GetChild(i).Find("Panel").gameObject.GetComponent<Image>().color = DataManager.Idle;
        }

        PlayerList.gameObject.transform.Find(GameManager.currentPlayer.Nickname).Find("Panel").GetComponent<Image>().color = DataManager.Green;
    }

    public static void AddScore(int playerIndex, int amount)
    {
        currentPlayer.Score += amount;
        PlayerList.gameObject.transform.Find(GameManager.currentPlayer.Nickname).Find("Score").GetComponent<Text>().text = currentPlayer.Score.ToString();
    }

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
