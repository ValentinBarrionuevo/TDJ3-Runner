using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float gravity;
    public Vector2 velocity;
    public float acceleration = 10;
    public float maxAcceleration = 10;
    public float maxXVelocity = 100;
    public float jumpVelocity = 20;
    public float groundHeight = 10;
    public bool isGrounded = true;
    public bool isHoldingJump = false;

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





    }

    private void FixedUpdate()
    {
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
                Ground ground = hit2D.collider.GetComponent<Ground>();
                    if (ground != null)
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
}
