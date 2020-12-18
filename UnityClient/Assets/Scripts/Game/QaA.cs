using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QaA : MonoBehaviour
{
    // Get question and answers from API

    public TMP_Text _question;
    public GameObject answers;
    public Button example;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SetQAinGame(AnswerType type, string question, string[] answers)
    {
        _question.text = question;

        switch (type)
        {
            case AnswerType.Closed:
                {
                    Button item = Instantiate(example);

                    break;
                }
            case AnswerType.Open:
                {
                    break;
                }
        }
    }
}

public enum AnswerType
{
    Closed,
    Open,
}
