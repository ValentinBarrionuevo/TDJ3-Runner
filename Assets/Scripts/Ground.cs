using UnityEngine;
using System.Collections;


public class Ground : MonoBehaviour
{

    public float groundHeight = 7;
    public float groundRight;
    public float screenRight;
    public float timer = 5;


    public int randomNumberGenerator = 1;

    bool didGenerateGround = false;

   // BoxCollider2D collider;

    Player player;

    Comms comms;

    public Obstacle obstacleTemplate;
    public Obstacle obstacleTemplate2;


    public paredPregunta paredPregunta;


    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        comms = GameObject.Find("Comms").GetComponent<Comms>();


       // collider = GetComponent<BoxCollider2D>();

        screenRight = Camera.main.transform.position.x * 2;

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        switch (player.distance)
        {
            case float i when i > 30 && i < 30.50f:
                comms.preguntado = true;
                StartCoroutine("askedBugFix");


                break;

            case float i when i > 60 && i < 60.50f:
                comms.preguntado = true;
                StartCoroutine("askedBugFix");

                break;

            case float i when i > 100 && i < 100.50f:
                comms.preguntado = true;
                StartCoroutine("askedBugFix");

                break;

            case float i when i > 130 && i < 130.50f:
                comms.preguntado = true;
                StartCoroutine("askedBugFix");

                break;

            case float i when i > 170 && i < 170.50f:
                comms.preguntado = true;
                StartCoroutine("askedBugFix");

                break;

            case float i when i > 200 && i < 200.50f:
                comms.preguntado = true;
                StartCoroutine("askedBugFix");

                break;

            case float i when i > 240 && i < 240.50f:
                comms.preguntado = true;
                StartCoroutine("askedBugFix");

                break;

            case float i when i > 270 && i < 270.50f:
                comms.preguntado = true;
                StartCoroutine("askedBugFix");

                break;

            case float i when i > 280 && i < 280.50f:
                comms.preguntado = true;
                StartCoroutine("askedBugFix");

                break;

            case float i when i > 290 && i < 290.50f:
                comms.preguntado = true;
                StartCoroutine("askedBugFix");

                break;

        }

    }

    private void FixedUpdate()
    {

        if (timer <= 0)
        {

            timer = 5;
            for (int i = 0; i < 1; i++)
            {
                randomNumberGenerator = Random.Range(1, 3);
            }
            //Debug.Log(randomNumberGenerator);

        }
        timer -= Time.fixedDeltaTime;

        Vector2 pos = transform.position;



        pos.x -= player.velocity.x * Time.fixedDeltaTime;



        groundRight = transform.position.x + (transform.localScale.x / 2);

        if (groundRight < 0)
        {
            Destroy(gameObject);
            return;
        }


        if (!didGenerateGround)
        {

            if (groundRight < screenRight)
            {
                didGenerateGround = true;
                generateGround();
            }

        }

        transform.position = pos;

    }

    private void generateGround()
    {
        ///////////////////////piso////////////////////////////////


        GameObject go = Instantiate(gameObject);
        BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        Vector2 pos;

        //pos.x = screenRight + ((gameObject.transform.localScale.x / 2) - 1);

        pos.x = screenRight + (goCollider.transform.right.x + (transform.localScale.x / 2));

        pos.y = transform.position.y;
        go.transform.position = pos;


        ////////////////////////obstaculos///////////////////////////////

        if (comms.preguntado == false)
        {
            switch (randomNumberGenerator)
            {
                case 1:

                    int obstacleNum = Random.Range(0, 2);

                    for (int i = 0; i < obstacleNum; i++)
                    {
                        GameObject box = Instantiate(obstacleTemplate.gameObject);
                        float y = 9.58f;
                        float halfWitdh = (65 / 2) - 5;
                        float left = go.transform.position.x - halfWitdh;
                        float right = go.transform.position.x + halfWitdh;

                        float x = Random.Range(left, right);

                        Vector2 boxPos = new Vector2(x, y);

                        box.transform.position = boxPos;
                        //Debug.Log(boxPos);
                    }

                    // Debug.Log("entre 1 y 2");

                    break;

                case 2:

                    int obstacleUpNum = Random.Range(1, 2);
                    for (int i = 0; i < obstacleUpNum; i++)
                    {
                        GameObject box = Instantiate(obstacleTemplate2.gameObject);
                        float y = 9.7f;
                        float halfWitdh = (65 / 2) - 5;
                        float left = go.transform.position.x - halfWitdh;
                        float right = go.transform.position.x + halfWitdh;

                        float x = Random.Range(left, right);

                        Vector2 boxPos = new Vector2(x, y);

                        box.transform.position = boxPos;
                        //Debug.Log(boxPos);
                    }

                    //  Debug.Log("entre 3 y 4");

                    break;
            }




        }
        else
        {
            if (comms.paredSpawn == false)
            {

                comms.paredSpawn = true;
                comms.preguntado = true;

                GameObject pared = Instantiate(paredPregunta.gameObject);
                float y = 12.8f;
                float halfWitdh = (65 / 2) - 1;
                float left = go.transform.position.x - halfWitdh;
                float right = go.transform.position.x + halfWitdh;

                float x = Random.Range(left, right);

                Vector2 boxPos = new Vector2(x, y);

                pared.transform.position = boxPos;

            }

        }






    }


    public IEnumerator askedBugFix()
    {
       // Debug.Log("esperando...");
        yield return new WaitForSecondsRealtime(1f);

        comms.preguntado = false;
        comms.paredSpawn = false;

       // Debug.Log("arreglado");

    }


    private void hit()
    {


    }


}