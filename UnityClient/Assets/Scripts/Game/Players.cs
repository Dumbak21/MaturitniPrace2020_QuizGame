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

        CreateList();
    }
    void Update()
    {
        
    }

    private List<Player> players = new List<Player>();

    public IList<Player> GetPlayers()
    {
        return players;
    }

    public void CreateList()
    {
        var players = GetPlayers();

        var trans = (RectTransform)PlayerList.transform;
        float width = trans.rect.width;
        float height = trans.rect.height;

        for (int i = 0; i < players.Count; i++)
        {

            GameObject item = Instantiate(example, PlayerList.transform);
            item.SetActive(true);
            item.name = players[i].Nickname;

            item.transform.localPosition += new Vector3((width/players.Count)*(i), 0, 0);
            (item.transform as RectTransform).sizeDelta = new Vector2(width / players.Count - 15, height); 


            //var trans2 = (RectTransform)item.transform;
            //var plWidth = width / players.Count;
            //trans2.rect.Set(plWidth * (i), 0 , plWidth, trans2.rect.height);

            //RectTransform original = item.GetComponent(typeof(RectTransform)) as RectTransform;
            //original.rect.Set(plWidth * (i), 0, plWidth, trans2.rect.height);

            //var pos = item.transform.position;
            //item.transform.position.Set((pos.x + (307f*(i))), pos.y, pos.z);

            var text = item.GetComponentInChildren<Text>();
            text.text = players[i].Nickname;

            Debug.Log(players[i].Nickname);
        }

    }
}

public class Player
{
    public int Id { get; set; }
    public string Nickname { get; set; }

    // public string Profile_picture { get; set; }
}
