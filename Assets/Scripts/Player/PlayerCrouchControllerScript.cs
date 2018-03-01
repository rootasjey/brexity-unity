using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchControllerScript : MonoBehaviour {

    public Transform groundCheckUp;
    public Animator anim;
    public LayerMask groundLayer;

    private float groundRadius = 0.2f;
    private bool isGroundedUp = false;
    private bool isGrounded = false;
    private bool isCrouched = false;
    private PlayerRunControllerScript playerRun;
    private PlayerJumpControllerScript playerJump;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerRun = GetComponent<PlayerRunControllerScript>();
        playerJump = GetComponent<PlayerJumpControllerScript>();
    }

    void Update()
    {
        isGrounded = playerJump.getGrounded();
        isGroundedUp = Physics2D.OverlapCircle(groundCheckUp.position, groundRadius, groundLayer);
        //Debug.Log("Get Button => "+ Input.GetButton("Crouch"));
        //Debug.Log("GroundedUp => "+!isGroundedUp);
        //Debug.Log("isGrounded => " + isGrounded);
        //Debug.Log("velo => "+GetComponent<Rigidbody2D>().velocity.y);
        if (Input.GetButtonDown("Crouch") && !isGroundedUp  && isGrounded && GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            isCrouched = !isCrouched;
            playerRun.canRun = !isCrouched;
            anim.SetBool("Crouched", isCrouched);
        }
    }

    public bool getCrouched()
    {
        return isCrouched;
    }
}
