using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurgerStaticDetectControllerScript : MonoBehaviour {

    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    public float distanceToCatchPlayer = 5f;
    public float height = 3f;

    public bool playerDetected = false;

    [Range(-1, 1)]
    public int direction = 1;

    private PlayerStats playerStats;
    private float minimumStealth = 50f;
    private void Start()
    {
         playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    void FixedUpdate()
    {

        direction *= -1;
        //Cast a ray to detect the player
        RaycastHit2D hitMiddle = Physics2D.Raycast(transform.position, transform.right * direction, distanceToCatchPlayer, playerLayer);
        Debug.DrawRay(transform.position, transform.right * distanceToCatchPlayer * direction, Color.green);

        RaycastHit2D hitTop = Physics2D.Raycast(transform.position, new Vector2(direction * distanceToCatchPlayer, height), Mathf.Sqrt(Mathf.Pow(distanceToCatchPlayer, 2) + Mathf.Pow(height, 2)), playerLayer);
        Debug.DrawRay(transform.position, new Vector2(direction * distanceToCatchPlayer, height), Color.green);

        RaycastHit2D hitBottom = Physics2D.Raycast(transform.position, new Vector2(direction * distanceToCatchPlayer, -height), Mathf.Sqrt(Mathf.Pow(distanceToCatchPlayer, 2) + Mathf.Pow(height, 2)), playerLayer);
        Debug.DrawRay(transform.position, new Vector2(direction * distanceToCatchPlayer, -height), Color.green);

        //If player is detected
        if (hitMiddle.collider != null || hitTop.collider != null || hitBottom.collider != null)
        {
            RaycastHit2D hit;

            if (hitMiddle.collider != null)
            {
                hit = hitMiddle;
            }else if(hitTop.collider != null)
            {
                hit = hitTop;
            }else
            {
                hit = hitBottom;
            }
            //Check if there is a obstacle between player and purger by casting another ray
            RaycastHit2D[] checkIfObstacles = Physics2D.RaycastAll(transform.position, hit.point - new Vector2(transform.position.x, transform.position.y), (hit.point - new Vector2(transform.position.x, transform.position.y)).magnitude, obstacleLayer);

            //If there is only one object, so there is no obstacle between player and purger
            if (checkIfObstacles.Length == 1 && playerStats.stealth < minimumStealth)
            {
                playerStats.Kill();
            }
        }

    }
}
