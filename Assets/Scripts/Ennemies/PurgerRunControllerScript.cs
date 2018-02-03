using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurgerRunControllerScript : MonoBehaviour {

    // Limit max speed for walk and run
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float actualSpeed = 0f;

    public bool playerDetected = false;
    public Vector2 playerLocation;
    public float rangeFollow = 2f;
    [Range(-1,1)]
    public int direction = 1;
    // Animator link to character
    //public Animator anim;

    // Use this for initialization
    void Start () {
        //anim = GetComponent<Animator>();
        actualSpeed = walkSpeed;
        playerLocation = Vector2.zero;
    }

    public void FixedUpdate()
    {
        if (playerDetected)
        {
            //Vector2.Distance(new Vector2(transform.position.x, transform.position.y), playerLocation);
            //if (range <= 15f)
            //{
            Vector2 direction = playerLocation - new Vector2(transform.position.x, transform.position.y);
            Debug.Log("direction => " + direction + " || range => " + rangeFollow + " || actualSpeed" + actualSpeed);
            if (direction.x > rangeFollow) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(actualSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction * actualSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
