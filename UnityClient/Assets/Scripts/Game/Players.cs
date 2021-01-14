using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Players : MonoBehaviour
{

    public GameObject PlayerList;
    public GameObject example;
    void Start()
    {
        //Load players from server
        players.Add(new Player { Id = 1, Nickname = "Karel" });
        players.Add(new Player { Id = 2, Nickname = "Petr" });
        players.Add(new Player { Id = 3, Nickname = "Marek" });

        queuedPlayers = CreateQueue();
        CreateList();

        GameManager.StartGame(queuedPlayers, ref PlayerList);
        GameManager.SetPlayerDesign();
    }
    void Update()
    {
        
    }

    

    private Player[] queuedPlayers;

    private List<Player> players = new List<Player>();

    public IList<Player> GetPlayers()
    {
        return players;
    }

    private void CreateList()
    {
        var players = queuedPlayers;

        var trans = (RectTransform)PlayerList.transform;
        float width = trans.rect.width;
        float height = trans.rect.height;

        for (int i = 0; i < players.Length; i++)
        {

            GameObject item = Instantiate(example, PlayerList.transform);
            item.SetActive(true);
            item.name = players[i].Nickname;

            item.transform.localPosition += new Vector3((width/players.Length) *(i), 0, 0);
            (item.transform as RectTransform).sizeDelta = new Vector2(width / players.Length - 15, height);

            //var comp = item.GetComponentInChildren<Text>();
            //comp.text = players[i].Nickname;

            item.transform.Find("Name").GetComponent<Text>().text = players[i].Nickname;
            item.transform.Find("Score").GetComponent<Text>().text = "0";


            Debug.Log(players[i].Nickname);
        }

    }

    private Player[] CreateQueue()
    {
        var players = GetPlayers();
        Debug.Log(players.Count);
        Player[] queuedPlayers = new Player[players.Count];
        List<int> randoms = new List<int>();
        for (int i = 0; i < players.Count; i++)
        {
            int ran;
            while (true)
            {
                ran = Random.Range(0, (players.Count));
                Debug.Log(ran);
                if (!randoms.Contains(ran))
                {
                    Debug.Log("vvv");
                    randoms.Add(ran);
                    break;
                }
            }

            Debug.Log("dddd");
            queuedPlayers[i] = players[ran];

        }
        return queuedPlayers;
    }

}

public class Player
{
    public int Id { get; set; }
    public int Score { get; set; }
    public string Nickname { get; set; }

    // public string Profile_picture { get; set; }
}
