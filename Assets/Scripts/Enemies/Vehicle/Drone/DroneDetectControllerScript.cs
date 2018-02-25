using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDetectControllerScript : MonoBehaviour {

    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    public float distanceToSeePlayer = 5f;
    public float rightX = 0.5f;
    public float leftX = -0.5f;
    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    void FixedUpdate()
    {
        //Cast a ray to detect the player
        RaycastHit2D hitMiddle = Physics2D.Raycast(transform.position, Vector2.down, distanceToSeePlayer, playerLayer);
        Debug.DrawRay(transform.position, Vector2.down * distanceToSeePlayer, Color.green);

        RaycastHit2D hitTop = Physics2D.Raycast(transform.position, new Vector2(rightX, -1).normalized, distanceToSeePlayer, playerLayer);
        Debug.DrawRay(transform.position, new Vector2(rightX, -1) * distanceToSeePlayer, Color.green);

        RaycastHit2D hitBottom = Physics2D.Raycast(transform.position, new Vector2(leftX, -1).normalized, distanceToSeePlayer, playerLayer);
        Debug.DrawRay(transform.position, new Vector2(leftX, -1) * distanceToSeePlayer, Color.green);

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
