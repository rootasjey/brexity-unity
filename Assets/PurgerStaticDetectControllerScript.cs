using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurgerStaticDetectControllerScript : MonoBehaviour {

    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    public float distanceToCatchPlayer = 5f;
    public float height = 0f;

    public bool playerDetected = false;

    [Range(-1, 1)]
    public int direction = 1;

    void FixedUpdate()
    {
        //Cast a ray to detect the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * direction, distanceToCatchPlayer, playerLayer);
        Debug.DrawRay(transform.position, transform.right * distanceToCatchPlayer * direction, Color.green);

        //If player is detected
        if (hit.collider != null)
        {
            //Check if there is a obstacle between player and purger by casting another ray
            RaycastHit2D[] checkIfObstacles = Physics2D.RaycastAll(transform.position, hit.point - new Vector2(transform.position.x, transform.position.y), (hit.point - new Vector2(transform.position.x, transform.position.y)).magnitude, obstacleLayer);

            //If there is only one object, so there is no obstacle between player and purger
            if (checkIfObstacles.Length == 1)
            {
                //Debug.Log("YOU ARE DEAD");
                var playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
                playerStats.Kill();
            }
        }

    }
}
