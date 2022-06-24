using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    Player player;
    Comms comms;
    Text distanceText;
    Text vidaText;



    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("distanceUI").GetComponent<Text>();
        vidaText = GameObject.Find("vidasUI").GetComponent<Text>();
        comms = GameObject.Find("Comms").GetComponent<Comms>();


    }
    void Start()
    { 

    }


    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + "m";

        vidaText.text = comms.life + "";
    }

    public void play()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);

    }

    public void credits()
    {
        SceneManager.LoadScene(3,LoadSceneMode.Single);

    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);

    }
}
