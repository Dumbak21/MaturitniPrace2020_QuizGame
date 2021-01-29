using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QaA : MonoBehaviour
{

    public TMP_Text _question;
    public GameObject answersBox;
    public GameObject openAnswersBox;
    public Button example;
    public Button ok;

    void Start()
    {
        // Get question and answers from API
        //string[] ans  = new string[4] { "1", "2", "3", "4" };
        //string que = "How many legs do elephants have";
        //SetQAinGame(AnswerType.Closed, que, ans );


        SetQAinGame(AnswerType.Open, "How long is 1 meter in meters");
    }

    void Update()
    {
        
    }


    void SetQAinGame(AnswerType type, string question, string[] answers = null)
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
                    answersBox.gameObject.SetActive(true);
                    openAnswersBox.gameObject.SetActive(false);

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
                    openAnswersBox.gameObject.SetActive(true);
                    answersBox.gameObject.SetActive(false);

                    ok.onClick.AddListener(() => { Answered(question, openAnswersBox.transform.Find("InputField").GetComponent<TMP_InputField>().text); });


                    if (GameManager.currentPlayer.Nickname == DataManager.NickName)
                    {
                        for (int i = 0; i < openAnswersBox.transform.Find("Numpad").childCount; i++)
                        {
                            openAnswersBox.transform.Find("Numpad").GetChild(i).GetComponent<Button>().interactable = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < openAnswersBox.transform.Find("Numpad").childCount; i++)
                        {
                            openAnswersBox.transform.Find("Numpad").GetChild(i).GetComponent<Button>().interactable = false;
                        }
                    }


                    break;
                }
        }



    }


    public void Answered(string question, string answear)
    {
        if (openAnswersBox.gameObject.activeSelf)
        {
            if (openAnswersBox.transform.Find("InputField").GetComponent<TMP_InputField>().text.EndsWith("."))
            {
                BackSpace();
            }
            answear = openAnswersBox.transform.Find("InputField").GetComponent<TMP_InputField>().text;
            openAnswersBox.transform.Find("InputField").GetComponent<TMP_InputField>().text = "";
        }
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


    public void OpenAddInput(int input)
    {
        openAnswersBox.transform.Find("InputField").GetComponent<TMP_InputField>().text += input.ToString();
    }
    public void BackSpace()
    {
        var text = openAnswersBox.transform.Find("InputField").GetComponent<TMP_InputField>().text;
        if(text.Length > 0)
        {
            openAnswersBox.transform.Find("InputField").GetComponent<TMP_InputField>().text = text.Remove((text.Length - 1), 1);
        }
    }
    public void OpenAddFloat()
    {
        if (openAnswersBox.transform.Find("InputField").GetComponent<TMP_InputField>().text.Contains("."))
        {
            return;
        }
        openAnswersBox.transform.Find("InputField").GetComponent<TMP_InputField>().text += ".";
    }

}

public enum AnswerType
{
    Closed,
    Open,
}
