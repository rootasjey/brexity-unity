using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpControllerScript : MonoBehaviour
{

    [Range(0, 100)]
    public float jumpForce;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator anim;
    public bool onLadder = false;

    private float groundRadius = 0.2f;
    private bool isGrounded = false;
    private PlayerRunControllerScript playerRun;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerRun = GetComponent<PlayerRunControllerScript>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        
        anim.SetBool("Grounded", isGrounded);
        anim.SetFloat("VerticalSpeed", GetComponent<Rigidbody2D>().velocity.y);
        playerRun.freezeMovement = !isGrounded && !onLadder;
        //Debug.Log("isGrounded => "+isGrounded+" || onLadder => "+onLadder);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump") && isGrounded && GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            //Debug.Log("Player Jump with a force of "+ jumpForce + " !");
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            anim.SetBool("Grounded", false);
        }
    }

    public bool getGrounded()
    {
        return isGrounded;
    }


}