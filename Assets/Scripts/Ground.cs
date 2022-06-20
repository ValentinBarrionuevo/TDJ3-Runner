using UnityEngine;

public class Ground : MonoBehaviour
{

    public float groundHeight = 7;
    public float groundRight;
    public float screenRight;
    public float timer = 5;
    public bool paredSpawn = false;
    public bool preguntado = false;

    public int randomNumberGenerator = 1;

    bool didGenerateGround = false;

    BoxCollider2D collider;
    
    Player player;

    public Obstacle obstacleTemplate;

    public paredPregunta paredPregunta;


    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        collider = GetComponent<BoxCollider2D>();

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
                 case float i when i > 100 && i < 150:
                        preguntado = true;
                     break;

                 default:
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
                randomNumberGenerator = Random.Range(1,3);
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

        if (preguntado == false)
        {
            switch (randomNumberGenerator)
            {
                case 1:

                    int obstacleNum = Random.Range(0, 4);

                    for (int i = 0; i < obstacleNum; i++)
                    {
                        GameObject box = Instantiate(obstacleTemplate.gameObject);
                        float y = 8;
                        float halfWitdh = (65 / 2) - 1;
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

                    int obstacleUpNum = Random.Range(0, 4);
                    for (int i = 0; i < obstacleUpNum; i++)
                    {
                        GameObject box = Instantiate(obstacleTemplate.gameObject);
                        float y = 12;
                        float halfWitdh = (65 / 2) - 1;
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
            if (paredSpawn==false)
            {
                paredSpawn = true;
                preguntado = true;

                GameObject pared = Instantiate(paredPregunta.gameObject);
                float y = 17;
                float halfWitdh = (65 / 2) - 1;
                float left = go.transform.position.x - halfWitdh;
                float right = go.transform.position.x + halfWitdh;
    
                float x = Random.Range(left, right);
        
                 Vector2 boxPos = new Vector2(x, y);

                pared.transform.position = boxPos;
            }

        }






    }

    private void hit()
    {


    }


}
