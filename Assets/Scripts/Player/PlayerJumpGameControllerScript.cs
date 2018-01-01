using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpGameControllerScript : MonoBehaviour {

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool applyCustomGravity = true;

    private PlayerJumpControllerScript playerJump;

    private void Start()
    {
        playerJump = GetComponent<PlayerJumpControllerScript>();
    }

    void Update()
    {
        if (applyCustomGravity) {
            if (GetComponent<Rigidbody2D>().velocity.y < 0 && !playerJump.getGrounded())
            {
                GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (GetComponent<Rigidbody2D>().velocity.y > 0 && !Input.GetButton("Jump"))
            {
                //Debug.Log("Player Jump with lowJumpMultiplier!");
                //Debug.Log("Velocity => "+ Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
                GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }
}
