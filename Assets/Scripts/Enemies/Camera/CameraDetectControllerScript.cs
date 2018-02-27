using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetectControllerScript : MonoBehaviour {

    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    public float distanceToSeePlayer = 7f;
    public float angleHeight = 5f;
    public float topCoef = 0.25f;
    public float bottomCoef =3f;
    private PlayerStats playerStats;

    [Range(-1, 1)]
    public int direction = 0;

    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    void FixedUpdate()
    {
        //Cast a ray to detect the player
        float angle = Mathf.Sqrt(Mathf.Pow(distanceToSeePlayer, 2) - Mathf.Pow(angleHeight, 2));
        RaycastHit2D hitMiddle = Physics2D.Raycast(transform.position, new Vector2(direction + angleHeight, -angle).normalized, angle, playerLayer);
        Debug.DrawRay(transform.position, new Vector2(direction + angleHeight, -angle).normalized * angle, Color.green);

        RaycastHit2D hitTop = Physics2D.Raycast(transform.position, new Vector2(direction + angleHeight, -angle * topCoef).normalized, angle, playerLayer);
        Debug.DrawRay(transform.position, new Vector2(direction + angleHeight , -angle * topCoef).normalized * angle, Color.green);

        RaycastHit2D hitBottom = Physics2D.Raycast(transform.position, new Vector2(direction + angleHeight, -angle * bottomCoef).normalized, angle, playerLayer);
        Debug.DrawRay(transform.position, new Vector2(direction + angleHeight, -angle * bottomCoef).normalized * angle, Color.green);

        //If player is detected
        if (hitMiddle.collider != null || hitTop.collider != null || hitBottom.collider != null)
        {
            RaycastHit2D hit;
            if (hitMiddle.collider != null)
            {
                hit = hitMiddle;
            }
            else if (hitTop.collider != null)
            {
                hit = hitTop;
            }
            else
            {
                hit = hitBottom;
            }
            // Check if there is a obstacle between player and purger by casting another ray
            RaycastHit2D[] checkIfObstacles = Physics2D.RaycastAll(transform.position, hit.point - new Vector2(transform.position.x, transform.position.y), (hit.point - new Vector2(transform.position.x, transform.position.y)).magnitude + 0.1f, obstacleLayer);
            // If there is only one object, so there is no obstacle between player and purger
            if (checkIfObstacles.Length == 1)
            {
              playerStats.Kill();
            }
        }
    }
}

