using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurgerRunControllerScript : MonoBehaviour {

    // Limit max speed for walk and run
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float actualSpeed = 0f;
    public Vector2 initialPosition;

    public bool playerDetected = false;
    public bool returnInitPosition = false;
    public Vector2 playerLocation;
    public float rangeFollow = 2f;
    [Range(-1,1)]
    public int direction = 1;

    // Use this for initialization
    void Start () {
        actualSpeed = walkSpeed;
        playerLocation = Vector2.zero;
        initialPosition = transform.position;
    }

    public void FixedUpdate()
    {
        if (playerDetected)
        {
            Vector2 directionToGoToPlayer = playerLocation - new Vector2(transform.position.x, transform.position.y);
            if (Mathf.Abs(directionToGoToPlayer.x) > rangeFollow) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(direction * actualSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else
        {
            if (returnInitPosition)
            {
                Vector2 directionToGoToInitPos = initialPosition - new Vector2(transform.position.x, transform.position.y);
                if (Mathf.Abs(directionToGoToInitPos.x) > 0.1 ||     Mathf.Abs(directionToGoToInitPos.x) < -0.1)
                {
                    if (!(directionToGoToInitPos.normalized.x * direction > 0))
                    {
                        direction *= -1;
                        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                    }

                    GetComponent<Rigidbody2D>().velocity = new Vector2(direction * actualSpeed, GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                { 
                    returnInitPosition = false;
                }
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(direction * actualSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
    }
}
