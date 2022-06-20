using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour
{
    Player player;

    public Questions[] Questions;
    private static List<Questions> unansweredQuestions;

    private Questions currentQuestion;

    [SerializeField]
    private Text questionText;

    private bool valor;


    private void Start()
    {
        

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = Questions.ToList<Questions>();
        }
    }
    private void Awake()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = Questions.ToList<Questions>(); 
        }

        setCurrentQuestion();
        Debug.Log(currentQuestion.question + " is " + currentQuestion.isCorrect);
    }
    void setCurrentQuestion()
    {

        int randomIndex = Random.Range(1    , unansweredQuestions.Count);
       // Debug.Log(randomIndex);
        currentQuestion = unansweredQuestions[randomIndex];

        questionText.text = currentQuestion.question;

        unansweredQuestions.RemoveAt(randomIndex);

        valor = currentQuestion.isCorrect;

    }

    public void userSelectTrue()
    {
        if (valor)
        {
            Debug.Log("correct");
            SceneManager.UnloadSceneAsync("QuestionsScene");
            Time.timeScale = 1;
                
        }
        else
        {
            Debug.Log("wrong");
            SceneManager.UnloadSceneAsync("QuestionsScene");
            Time.timeScale = 1;



        }
    }
    public void userSelectFalse()
    {
        if (!valor)
        {
            Debug.Log("correct");
            SceneManager.UnloadSceneAsync("QuestionsScene");
            Time.timeScale = 1;



        }
        else
        {
            Debug.Log("wrong");
            SceneManager.UnloadSceneAsync("QuestionsScene");
            Time.timeScale = 1;



        }
    } 
}
