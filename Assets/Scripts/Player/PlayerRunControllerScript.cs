using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunControllerScript : MonoBehaviour {

    // Limit max speed for walk and run
    public float maxWalkSpeed = 2f;
    public float maxRunSpeed = 5f;
    public float actualSpeed = 0f;
    // Set character orientation
    public bool facingRight = true;
    // Animator link to character
    public Animator anim;
    // Freeze velocity on movement
    public bool freezeMovement = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Fixed update
	void FixedUpdate () {

        if (freezeMovement)
        {
            return;
        }
        float move = Input.GetAxis("Horizontal");
    
        // Sprint
        if (Input.GetButton("Sprint"))
        {
            actualSpeed = maxRunSpeed * move;
        }
        else
        {
            actualSpeed = maxWalkSpeed * move;
        }
        // Set speed for animator
        anim.SetFloat("Speed", Mathf.Abs(actualSpeed));
        // Set velocity to object
        GetComponent<Rigidbody2D>().velocity = new Vector2(actualSpeed, GetComponent<Rigidbody2D>().velocity.y);
        
        //Verify orientation and adjust if needed
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    /**
     * Function to change character orientation
     */
    void Flip()
    {
        GetComponent<SpriteRenderer>().flipX = facingRight;
        facingRight = !facingRight;
    }
}
