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
            //Debug.Log("direction => " + directionToGoToPlayer + " || range => " + rangeFollow + " || actualSpeed" + actualSpeed+"  || direction.x => "+ directionToGoToPlayer.x +">"+ "rangeFollow => "+rangeFollow);
            if (Mathf.Abs(directionToGoToPlayer.x) > rangeFollow) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(direction * actualSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else
        {
            // Debug.Log("return in position => " + returnInitPosition);
            if (returnInitPosition)
            {
                Vector2 directionToGoToInitPos = initialPosition - new Vector2(transform.position.x, transform.position.y);
                //Debug.Log("direction => " + directionToGoToInitPos + " || actualSpeed" + actualSpeed + " || direction to Go => " + Mathf.Abs(directionToGoToInitPos.x));
                if (Mathf.Abs(directionToGoToInitPos.x) > 0.1 ||     Mathf.Abs(directionToGoToInitPos.x) < -0.1)
                {
                    //Debug.Log("direction to Go Normalize x => " + directionToGoToInitPos.normalized.x);
                    //Debug.Log("direction => "+ direction);
                    //Debug.Log("condition => " + directionToGoToInitPos.normalized.x * direction);
                    if (!(directionToGoToInitPos.normalized.x * direction > 0))
                    {
                        //Debug.Log("FLIP");
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
