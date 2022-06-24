using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{

    private Vector2 startTouchPosition, endTouchPosition;
    private Vector2 startTouchPosition1, endTouchPosition1;

    public float gravity;
    public Vector2 velocity;
    public float acceleration = 10;
    public float maxAcceleration = 10;
    public float maxXVelocity = 100;
    public float jumpVelocity = 30;
    public float groundHeight = 7;
    public bool isGrounded = true;
    public bool isHoldingJump = false;
    public float distance = 70;
    private bool pressZ = false;
    public Animator animator; 
    public bool touched = false;
    private bool startJump = false;
    private bool startDash = false;

    public float timer = 0.2f;

    public float jumpThreshold = 2;

    Comms comms;

    private void Awake()
    {
        comms = GameObject.Find("Comms").GetComponent<Comms>();

        //comms.index = SceneManager.GetActiveScene().buildIndex;

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

    }

    void Update()
    {

        if (distance >= 300)
        {
            death();
        }

        swipeUpCheck();
        swipeDownCheck();

        animator.SetFloat("speed", Mathf.Abs(velocity.x));

        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);


        if (isGrounded || groundDistance <= jumpThreshold)
        {
            if (startJump==true)
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                StartCoroutine("jump");

            }

        }


        if (comms.life == 0)
        {
            death();
        }


    }

    private void FixedUpdate()
    {

        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0)) && touched == false)
        {
            touched = true;
            

        } else if (touched == false)
        {
            velocity.x = 0;
        }


        if (Input.GetKey(KeyCode.Space))
        {
            startJump = true;
        }

        distance += (velocity.x * Time.fixedDeltaTime) * 0.07f;

        Vector2 pos = transform.position;

        if (!isGrounded)
        {
            pos.y += velocity.y * Time.fixedDeltaTime;

            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;

            }

            Vector2 rayOrigin = new Vector2(pos.x + 0.7f, (pos.y - 2.1f));
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if (hit2D.collider != null)
            {
                PisoCollider PisoCollider = hit2D.collider.GetComponent<PisoCollider>();
                if (PisoCollider != null)
                {
                    groundHeight = hit2D.point.y;
                    pos.y = groundHeight + (transform.localScale.y / 2);
                    velocity.y = 0;
                    isGrounded = true;
                    timer = 0.2f;
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

        }

        if (isGrounded && touched == true)
        {
            float velocityRatio = velocity.x / maxXVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);

            velocity.x += acceleration * Time.fixedDeltaTime;
            if (velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }

            Vector2 rayOrigin = new Vector2(pos.x - 0.7f, (pos.y - 2.1f));
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);

            if (hit2D.collider != null)
            {
                isGrounded = false;
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.black);

        }


        Vector2 obstOrigin = new Vector2(pos.x + (transform.localScale.x / 2), (pos.y - (transform.localScale.y / 2)));
        RaycastHit2D obstHitX = Physics2D.Raycast(obstOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime);
        if (obstHitX.collider != null)
        {

            paredPregunta pared = obstHitX.collider.GetComponent<paredPregunta>();

            Obstacle obstacle = obstHitX.collider.GetComponent<Obstacle>();

            if (obstacle != null)
            {
                hit(obstacle);
            }
            else if (pared != null)
            {

                Destroy(pared.gameObject);
                SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
                Time.timeScale = 0;

            }
        }

        ///////////////////////////////////////// check collide up//////////////////////////////////////

        if ((Input.GetKey("z") || startDash == true) && pressZ == false && isGrounded)
        {
            Debug.Log("dash");
            StartCoroutine("dash");

        }
        else if(pressZ != true) 
        {

            Vector2 obstOrigin1 = new Vector2((pos.x + (transform.localScale.x /2 )) , (pos.y + (transform.localScale.y / 2) - 3));
            RaycastHit2D obstHitX1 = Physics2D.Raycast(obstOrigin1, Vector2.right, velocity.x * Time.fixedDeltaTime);
            if (obstHitX1.collider != null)
            {

                paredPregunta pared = obstHitX1.collider.GetComponent<paredPregunta>();

                Obstacle obstacle = obstHitX1.collider.GetComponent<Obstacle>();

                if (obstacle != null)
                { 
                    hit(obstacle);
                }
                else if (pared != null)
                {

                    Destroy(pared.gameObject);
                    SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
                    Time.timeScale = 0;

                }
            }

        }

        ///////////////////////////////////////// check collide up//////////////////////////////////////




        RaycastHit2D obstHitY = Physics2D.Raycast(obstOrigin, Vector2.right, velocity.y * Time.fixedDeltaTime);
        if (obstHitY.collider != null)
        {
            Obstacle obstacle = obstHitY.collider.GetComponent<Obstacle>();
            paredPregunta pared = obstHitX.collider.GetComponent<paredPregunta>();


            if (obstacle != null)
            {
                hit(obstacle);
            }
            else if (pared != null)
            {

                Destroy(pared.gameObject);
                SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
                Time.timeScale = 0;

            }
        }



        transform.position = pos;
    }



    private void death()
    {
        Destroy(gameObject);
        Time.timeScale = 0;
        comms.life = 3;
        comms.preguntado = false;
        comms.paredSpawn = false;
    SceneManager.LoadScene(3, LoadSceneMode.Single);

    }

    public void hit(Obstacle obstacle)
    {
        Destroy(obstacle.gameObject);
        velocity.x *= 0.6f;
        comms.life -= 1;
        Debug.Log(comms.life);
    }

    public IEnumerator dash()
    {
        Debug.Log("abajo");
        animator.SetBool("dash?", true);
        startDash = false;
        pressZ = true;
        yield return new WaitForSecondsRealtime(0.8f);
        animator.SetBool("dash?", false);
        pressZ = false;
        Debug.Log("arriba");
    }

    public IEnumerator jump()
    {
        startJump = false;
        isHoldingJump = true;
        yield return new WaitForSecondsRealtime(0.1f);
        isHoldingJump = false;
       // isGrounded = true;

    }

    private void swipeUpCheck()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;

        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
            if (endTouchPosition.y > startTouchPosition.y)
            {
                startJump = true;
            }
        }
        
    }

    private void swipeDownCheck()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition1 = Input.GetTouch(0).position;

        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition1 = Input.GetTouch(0).position;
            if (endTouchPosition1.y < startTouchPosition1.y)
            {
                startDash = true;
            }
        }

    }


}
