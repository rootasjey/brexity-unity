using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * date : 31/12/2017
 * Layers documentation : https://docs.unity3d.com/Manual/Layers.html
 * Example: https://answers.unity.com/questions/50279/check-if-layer-is-in-layermask.html
 * */
public class PlayerClimbControllerScript : MonoBehaviour {

    public LayerMask ladderLayer;
    public float maxClimbSpeed = 1f;

    private void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Stops the player from being affected by gravity while on ladder
        if (ladderLayer.value == (ladderLayer.value | (1 << collision.gameObject.layer)) && Input.GetAxis("Vertical") != 0)
        { 
            collision.gameObject.GetComponent<Animator>().SetBool("OnLadder", true);
            collision.gameObject.GetComponent<Animator>().SetFloat("VerticalTransition", 0);
            collision.gameObject.GetComponent<PlayerRunControllerScript>().freezeMovement = false;
            collision.gameObject.GetComponent<PlayerJumpControllerScript>().onLadder = true;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (ladderLayer.value == (ladderLayer.value | (1 << collision.gameObject.layer)))
        { 
            float verticalValue = Input.GetAxis("Vertical");
            if (verticalValue != 0)
            {
                //Debug.Log("Player On ladder with vertical axis "+ verticalValue + "!");
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
                collision.gameObject.GetComponent<PlayerJumpGameControllerScript>().applyCustomGravity = false;
                collision.gameObject.GetComponent<PlayerJumpControllerScript>().onLadder = true;
                collision.gameObject.GetComponent<Animator>().SetBool("OnLadder", true);
                collision.gameObject.GetComponent<Animator>().SetFloat("VerticalTransition", verticalValue);
                collision.transform.Translate(Vector2.up * verticalValue * maxClimbSpeed * Time.deltaTime);
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        // Stops the player from not being affected by gravity after exit on ladder
        if (ladderLayer.value == (ladderLayer.value | (1 << collision.gameObject.layer)))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            collision.gameObject.GetComponent<PlayerJumpGameControllerScript>().applyCustomGravity = true;
            collision.gameObject.GetComponent<PlayerJumpControllerScript>().onLadder = false;
            collision.gameObject.GetComponent<Animator>().SetBool("OnLadder", false);
            collision.gameObject.GetComponent<Animator>().SetFloat("VerticalTransition", 0);
        }

    }
}
