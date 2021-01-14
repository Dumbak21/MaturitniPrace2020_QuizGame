using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QaA : MonoBehaviour
{

    public TMP_Text _question;
    public GameObject answersBox;
    public Button example;

    void Start()
    {
        // Get question and answers from API
        string[] ans  = new string[4] { "1", "2", "3", "4" };
        string que = "How many legs do elephants have";
        SetQAinGame(AnswerType.Closed, que, ans );
    }

    void Update()
    {
        
    }


    void SetQAinGame(AnswerType type, string question, string[] answers)
    {
        //var gm = FindObjectOfType<GameManager>();

        _question.text = question;
        _question.gameObject.SetActive(true);

        var trans = (RectTransform)answersBox.transform;
        float width = trans.rect.width;
        float height = trans.rect.height;

        switch (type)
        {
            case AnswerType.Closed:
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Button item = Instantiate(example, answersBox.transform);

                        item.GetComponentInChildren<TMP_Text>().text = answers[i];
                        item.name = i.ToString();
                        int index = i;
                        item.onClick.AddListener(() => { Debug.Log(index); Answered(question, answers[index]); });

                        if(GameManager.currentPlayer.Nickname == DataManager.NickName)
                        {
                            item.interactable = true;
                        }
                        else
                        {
                            item.interactable = false;
                        }

                        if (i < 2)
                        {
                            item.transform.localPosition += new Vector3((width / 2) * (i) + 15, (height/4) , 0);
                            (item.transform as RectTransform).sizeDelta = new Vector2((width / 2) - 30, height/2 - 30);
                        }
                        else
                        {
                            item.transform.localPosition += new Vector3((width / 2) * (i-2) + 15, -(height / 4), 0);
                            (item.transform as RectTransform).sizeDelta = new Vector2(width / 2 - 30, height/2 - 30);
                        }

                        item.gameObject.SetActive(true);


                    }


                    break;
                }
            case AnswerType.Open:
                {
                    break;
                }
        }



    }


    public void Answered(string question, string answear)
    {
        bool right = true;
        //check if ans is right
        //AskServer(question, ans) => true/false
        //if true
        if (right)
        {
            GameManager.AddScore(GameManager.currentPlayer.Id, 100);
        }
        GameManager.NextPlayer();

        //get new QaA from server
        string[] ans = new string[4] { "a", "b", "c", "z" };
        SetQAinGame(AnswerType.Closed, "What is the first letter of alphabet", ans);
    }


}

public enum AnswerType
{
    Closed,
    Open,
}
