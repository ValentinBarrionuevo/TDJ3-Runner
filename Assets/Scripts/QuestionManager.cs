using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;

public class QuestionManager : MonoBehaviour
{
    public Comms comms;

    public Questions[] Questions;
    public Questions[] Questions2;
    public Questions[] Questions3;


    private Questions currentQuestion;

    [SerializeField]
    private Text questionText;

    public bool valor = true;

    

    private void Awake()
    {

        comms = GameObject.Find("Comms").GetComponent<Comms>();

        switch (comms.index)
        {
            case 0:

                if (comms.unansweredQuestions == null || comms.unansweredQuestions.Count == 0)
                {
                    comms.unansweredQuestions = Questions.ToList<Questions>();
                }

                break;
            case 2:

                if (comms.unansweredQuestions == null || comms.unansweredQuestions.Count == 0)
                {
                    comms.unansweredQuestions = Questions2.ToList<Questions>();
                }

                break;
             case 3:

                if (comms.unansweredQuestions == null || comms.unansweredQuestions.Count == 0)
                {
                    comms.unansweredQuestions = Questions3.ToList<Questions>();
                }

                break;
        }


        


        setCurrentQuestion();
        Debug.Log(currentQuestion.question + " is " + currentQuestion.isCorrect);
        valor = currentQuestion.isCorrect;
        Debug.Log(valor);
    }
    void setCurrentQuestion()
    {

        comms.unansweredQuestions.ForEach(Console.WriteLine);


        int randomIndex = UnityEngine.Random.Range(1    , comms.unansweredQuestions.Count);
       // Debug.Log(randomIndex);
        currentQuestion = comms.unansweredQuestions[randomIndex];

        questionText.text = currentQuestion.question;

        comms.unansweredQuestions.RemoveAt(randomIndex);




    }

    public void userSelectTrue()
    {
        Debug.Log(valor);

        if (valor == true)
        {
            Debug.Log("correct");
            Time.timeScale = 1;
            comms.paredSpawn = false;
           

            SceneManager.UnloadSceneAsync("QuestionsScene");

        }
        else
        {
            Debug.Log("wrongTrue");
            Time.timeScale = 1;
            comms.GetComponent<Comms>().preguntado = false;
            comms.GetComponent<Comms>().paredSpawn = false;
            comms.damage();
            SceneManager.UnloadSceneAsync("QuestionsScene");


        }
    }
    public void userSelectFalse()
    {
        if (valor == false)
        {
            Debug.Log("correct");
            Time.timeScale = 1;
            comms.GetComponent<Comms>().preguntado = false;
            comms.GetComponent<Comms>().paredSpawn = false;
            SceneManager.UnloadSceneAsync("QuestionsScene");


        }
        else
        {
            Debug.Log("wrong");
            Time.timeScale = 1;
            comms.GetComponent<Comms>().preguntado = false;
            comms.GetComponent<Comms>().paredSpawn = false;
            comms.damage();
            SceneManager.UnloadSceneAsync("QuestionsScene");


        }
    } 
}
