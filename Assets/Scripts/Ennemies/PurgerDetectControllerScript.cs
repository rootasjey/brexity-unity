using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurgerDetectControllerScript : MonoBehaviour {

    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    public LayerMask purgerWall;

    public float distanceToSeePlater = 5f;
    public float distanceToSeeObstacle = 2f;
    public float height = 0f;

    public float detectionTimer = 0;
    public float maxDetectionTimer = 5f;

    public Vector2 purgerDirection = Vector2.right;

    void FixedUpdate()
    {
        //Cast a ray to detect the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * GetComponent<PurgerRunControllerScript>().direction, distanceToSeePlater, playerLayer);
        Debug.DrawRay(transform.position, transform.right * distanceToSeePlater * GetComponent<PurgerRunControllerScript>().direction, Color.green);

        //RaycastHit2D hitTop = Physics2D.Raycast(transform.position, new Vector2(distanceToSee, height), new Vector2(distanceToSee, height).magnitude/*Mathf.Sqrt(Mathf.Pow(distanceToSee, 2)+ Mathf.Pow(height, 2))*/, playerLayer);
        //Debug.DrawRay(transform.position, new Vector2(distanceToSee, height), Color.green);


        //RaycastHit2D hitBottom = Physics2D.Raycast(transform.position, new Vector2(distanceToSee, -height), Mathf.Sqrt(Mathf.Pow(distanceToSee, 2) + Mathf.Pow(height, 2)), playerLayer);
        //Debug.DrawRay(transform.position, new Vector2(distanceToSee, -height), Color.green);
        
        //Debug.Log(detectionTimer);

        //If player is detected
        if (hit.collider != null)
        {
            //Debug.Log("I'm at =>" + new Vector2(transform.position.x, transform.position.y));
            //Debug.Log("Collider Point Distance => " + (hit.point - new Vector2(transform.position.x, transform.position.y)));
            //Debug.Log("Magnitude => "+ (hit.point - new Vector2(transform.position.x, transform.position.y)).magnitude);

            //Check if there is a obstacle between player and purger by casting another ray
            RaycastHit2D[] checkIfObstacles = Physics2D.RaycastAll(transform.position, hit.point - new Vector2(transform.position.x, transform.position.y), (hit.point - new Vector2(transform.position.x, transform.position.y)).magnitude, obstacleLayer);
            //Debug.Log(hit.collider.gameObject.GetComponent<PlayerVisibilityControllerScript>().visibility);
            //Debug.Log("Player Detected ");
            
            //If there is only one object, so there is no obstacle between player and purger
            if (checkIfObstacles.Length == 1)
            {
                //set player detected to true, up his speed, save player location, and set detection timer
                Debug.Log("Player Detected and check if there is a obstacle => "+checkIfObstacles.Length);
                GetComponent<PurgerRunControllerScript>().playerDetected = true;
                GetComponent<PurgerRunControllerScript>().actualSpeed = GetComponent<PurgerRunControllerScript>().runSpeed;
                GetComponent<PurgerRunControllerScript>().playerLocation = hit.point;
                detectionTimer = maxDetectionTimer;
                
            }
            else
            {
                //if there is a obstacle so purger can't go there and timer decrease
                detectionTimer = (detectionTimer <= 0)? 0 : detectionTimer - 1 * Time.fixedDeltaTime;
            }
        }
        else
        {
            //by default decrease timer if nothing detected
            detectionTimer = (detectionTimer <= 0) ? 0 : detectionTimer - 1 * Time.fixedDeltaTime;
        }

        //If player was detected by timer hit 0, so purger intern alert end
        if (GetComponent<PurgerRunControllerScript>().playerDetected && detectionTimer == 0)
        {
            GetComponent<PurgerRunControllerScript>().playerDetected = false;
            GetComponent<PurgerRunControllerScript>().actualSpeed = GetComponent<PurgerRunControllerScript>().walkSpeed;
        }

        //Cast a ray to detect his environment limit 
        RaycastHit2D hitObstacle = Physics2D.Raycast(transform.position, purgerDirection, distanceToSeeObstacle, purgerWall);
        Debug.DrawRay(transform.position, purgerDirection * distanceToSeeObstacle, Color.red);

        //If purger meet a limitation and he is not chasing player so change direction
        if (hitObstacle.collider != null && !GetComponent<PurgerRunControllerScript>().playerDetected)
        {
            //set purger direction to his opposite
            purgerDirection = purgerDirection * -1;
            GetComponent<PurgerRunControllerScript>().direction *= -1;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
        
    }

}
