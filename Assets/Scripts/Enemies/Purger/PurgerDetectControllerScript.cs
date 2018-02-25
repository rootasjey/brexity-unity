using UnityEngine;

public class PurgerDetectControllerScript : MonoBehaviour {

    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    public LayerMask purgerWall;

    public float distanceToSeePlayer = 5f;
    public float deadlySight = 2f;
    public float distanceToSeeObstacle = 2f;
    public float height = 3f;

    public float detectionTimer = 0;
    public float envDetectionTimer = 0;
    public float maxDetectionTimer = 5f;
    public float maxEnvDetectionTimer = 2f;

    public float minimumStealth = 50f;

    public Vector2 purgerDirection = Vector2.right;

    private PlayerStats playerStats;

    private void Start()
    {
       playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    void FixedUpdate()
    {
        //Cast a ray to detect the player
        RaycastHit2D hitMiddle = Physics2D.Raycast(transform.position, transform.right * GetComponent<PurgerRunControllerScript>().direction, distanceToSeePlayer, playerLayer);
        Debug.DrawRay(transform.position, transform.right * distanceToSeePlayer * GetComponent<PurgerRunControllerScript>().direction, Color.green);

        RaycastHit2D hitTop = Physics2D.Raycast(transform.position, new Vector2(GetComponent<PurgerRunControllerScript>().direction * distanceToSeePlayer, height), Mathf.Sqrt(Mathf.Pow(distanceToSeePlayer, 2) + Mathf.Pow(height, 2)), playerLayer);
        Debug.DrawRay(transform.position, new Vector2(GetComponent<PurgerRunControllerScript>().direction * distanceToSeePlayer, height), Color.green);

        RaycastHit2D hitBottom = Physics2D.Raycast(transform.position,  new Vector2(GetComponent<PurgerRunControllerScript>().direction * distanceToSeePlayer, -height), Mathf.Sqrt(Mathf.Pow(distanceToSeePlayer, 2) + Mathf.Pow(height, 2)), playerLayer);
        Debug.DrawRay(transform.position, new Vector2(GetComponent<PurgerRunControllerScript>().direction * distanceToSeePlayer, -height), Color.green);
        
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
           
            //Check if there is a obstacle between player and purger by casting another ray
            RaycastHit2D[] checkIfObstacles = Physics2D.RaycastAll(transform.position, hit.point - new Vector2(transform.position.x, transform.position.y), (hit.point - new Vector2(transform.position.x, transform.position.y)).magnitude, obstacleLayer);
            
            //If there is only one object, so there is no obstacle between player and purger
            if (checkIfObstacles.Length == 1 && playerStats.stealth < minimumStealth)
            {

                Vector2 directionToGoToPlayer = hit.point - new Vector2(transform.position.x, transform.position.y);
                if (Mathf.Abs(directionToGoToPlayer.x) < deadlySight)
                {
                  playerStats.Kill();
                }
                //set player detected to true, up his speed, save player location, and set detection timer
                GetComponent<PurgerRunControllerScript>().playerDetected = true;
                GetComponent<PurgerRunControllerScript>().returnInitPosition = false;
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
            GetComponent<PurgerRunControllerScript>().returnInitPosition = true;
            GetComponent<PurgerRunControllerScript>().actualSpeed = GetComponent<PurgerRunControllerScript>().walkSpeed;
        }

        //Cast a ray to detect his environment limit 
        RaycastHit2D hitObstacle = Physics2D.Raycast(transform.position, transform.right * GetComponent<PurgerRunControllerScript>().direction, distanceToSeeObstacle, purgerWall);
        Debug.DrawRay(transform.position, transform.right * GetComponent<PurgerRunControllerScript>().direction * distanceToSeeObstacle, Color.red);

        //If purger meet a limitation and he is not chasing player so change direction
        if (hitObstacle.collider != null && !GetComponent<PurgerRunControllerScript>().playerDetected && !GetComponent<PurgerRunControllerScript>().returnInitPosition)
        {
            if (envDetectionTimer == 0) {
                //set purger direction to his opposite
                envDetectionTimer = maxEnvDetectionTimer;
                GetComponent<PurgerRunControllerScript>().direction *= -1;
                GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                GetComponent<PurgerRunControllerScript>().actualSpeed = GetComponent<PurgerRunControllerScript>().walkSpeed;
            }
            else
            {
                GetComponent<PurgerRunControllerScript>().actualSpeed = 0;
                envDetectionTimer = (envDetectionTimer <= 0) ? 0 : envDetectionTimer - 1 * Time.fixedDeltaTime;
            }
        } else
        {
            envDetectionTimer = maxEnvDetectionTimer;
        }
        
    }

}
