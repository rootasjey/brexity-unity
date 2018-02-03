using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurgerDetectControllerScript : MonoBehaviour {

    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    public float distanceToSeePlater = 5f;
    public float distanceToSeeObstacle = 2f;
    public float height = 0f;

    public float detectionTimer = 0;
    public float maxDetectionTimer = 5f;

    public Vector2 purgerDirection = Vector2.right;

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, distanceToSeePlater, playerLayer);
        Debug.DrawRay(transform.position, transform.right * distanceToSeePlater, Color.green);

        //RaycastHit2D hitTop = Physics2D.Raycast(transform.position, new Vector2(distanceToSee, height), new Vector2(distanceToSee, height).magnitude/*Mathf.Sqrt(Mathf.Pow(distanceToSee, 2)+ Mathf.Pow(height, 2))*/, playerLayer);
        //Debug.DrawRay(transform.position, new Vector2(distanceToSee, height), Color.green);


        //RaycastHit2D hitBottom = Physics2D.Raycast(transform.position, new Vector2(distanceToSee, -height), Mathf.Sqrt(Mathf.Pow(distanceToSee, 2) + Mathf.Pow(height, 2)), playerLayer);
        //Debug.DrawRay(transform.position, new Vector2(distanceToSee, -height), Color.green);
        
        //Debug.Log(detectionTimer);
        if (hit.collider != null)
        {
            //Debug.Log("I'm at =>" + new Vector2(transform.position.x, transform.position.y));
            //Debug.Log("Collider Point Distance => " + (hit.point - new Vector2(transform.position.x, transform.position.y)));
            //Debug.Log("Magnitude => "+ (hit.point - new Vector2(transform.position.x, transform.position.y)).magnitude);
            RaycastHit2D[] checkIfObstacles = Physics2D.RaycastAll(transform.position, hit.point - new Vector2(transform.position.x, transform.position.y), (hit.point - new Vector2(transform.position.x, transform.position.y)).magnitude, obstacleLayer);
            //Debug.Log(hit.collider.gameObject.GetComponent<PlayerVisibilityControllerScript>().visibility);
            if (checkIfObstacles.Length == 1)
            {
                GetComponent<PurgerRunControllerScript>().playerDetected = true;
                GetComponent<PurgerRunControllerScript>().actualSpeed = GetComponent<PurgerRunControllerScript>().runSpeed;
                GetComponent<PurgerRunControllerScript>().playerLocation = hit.point;
                detectionTimer = maxDetectionTimer;
                //Debug.Log(" Obstacle => " + checkIfObstacles.Length);
                //Debug.Log("Another Point Distance => " + (new Vector2(transform.position.x, transform.position.y) - checkIfObstacle.point));
            }
            else
            {
                detectionTimer = (detectionTimer <= 0)? 0 : detectionTimer - 1 * Time.fixedDeltaTime;
            }
        }
        else
        {
            detectionTimer = (detectionTimer <= 0) ? 0 : detectionTimer - 1 * Time.fixedDeltaTime;
        }

        if (GetComponent<PurgerRunControllerScript>().playerDetected && detectionTimer == 0)
        {
            GetComponent<PurgerRunControllerScript>().playerDetected = false;
        }

        RaycastHit2D hitObstacle = Physics2D.Raycast(transform.position, purgerDirection, distanceToSeeObstacle, obstacleLayer);
        Debug.DrawRay(transform.position, purgerDirection * distanceToSeeObstacle, Color.red);

        if (hitObstacle.collider != null)
        {
            purgerDirection = purgerDirection * -1;
            GetComponent<PurgerRunControllerScript>().direction *= -1;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
        
    }

}
