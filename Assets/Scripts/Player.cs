using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    public float gravity;
    public Vector2 velocity;
    public float acceleration = 10;
    public float maxAcceleration = 10;
    public float maxXVelocity = 100;
    public float jumpVelocity = 20;
    public float groundHeight = 7;
    public bool isGrounded = true;
    public bool isHoldingJump = false;
    public float distance = 70;
    public int life = 3;

    public float timer = 0.2f;

    public float jumpThreshold = 2;

    

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);


        if (isGrounded || groundDistance <= jumpThreshold)
        {
            if (Input.GetKeyDown(KeyCode.Space))
               {
                    isGrounded = false;
                    velocity.y = jumpVelocity;
                    isHoldingJump = true;
              }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }


        if (life == 0)
        {
            death();
        }


    }

    private void FixedUpdate()
    {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
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

            Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if (hit2D.collider != null)
            {
                PisoCollider PisoCollider = hit2D.collider.GetComponent<PisoCollider>();
                if (PisoCollider != null)
                {
                    groundHeight = hit2D.point.y;
                    pos.y = groundHeight;
                    velocity.y = 0;
                    isGrounded = true;
                    timer = 0.2f;
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

        }

        if (isGrounded)
        {
            float velocityRatio = velocity.x / maxXVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);

            velocity.x += acceleration * Time.fixedDeltaTime;
            if (velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }

            Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if (hit2D.collider == null)
            {
                isGrounded = false;
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);

        }

        if (isHoldingJump)
        {
            jumpTimer();
        }

        Vector2 obstOrigin = new Vector2(pos.x, pos.y);
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

                SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
                Time.timeScale = 0.2f;
            }
        }

        RaycastHit2D obstHitY = Physics2D.Raycast(obstOrigin, Vector2.right, velocity.y * Time.fixedDeltaTime);
        if (obstHitY.collider != null)
        {
            Obstacle obstacle = obstHitY.collider.GetComponent<Obstacle>();

            if (obstacle != null)
            {
                hit(obstacle);
            } 
        }



        transform.position = pos;
    }

    private void jumpTimer()
    {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            isHoldingJump = false;
        }
    }

    private void death()
    {
        Destroy(gameObject);
        Time.timeScale = 0;

       // SceneManager.LoadScene("GameOver");
    }
    private void changeLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void hit(Obstacle obstacle)
    {
        Destroy(obstacle.gameObject);
        velocity.x *= 0.6f;
        life -= 1;
        Debug.Log(life);
    }

}
