using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comms : MonoBehaviour
{
    public int life = 3;
    public bool preguntado = false;
    public bool paredSpawn = false;

    public int index;

    public List<Questions> unansweredQuestions;

  



    public void damage()
    {
        life -= 1;
        Debug.Log("da;o");

    }



}
