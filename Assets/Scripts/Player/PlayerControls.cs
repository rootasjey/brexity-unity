using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    public Rigidbody2D rb;
    public float movespeed;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        movespeed = 5;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            rb.velocity = new Vector2(-movespeed, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            rb.velocity = new Vector2(movespeed, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.UpArrow)) {
            rb.velocity = new Vector2(rb.velocity.x, movespeed);
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            rb.velocity = new Vector2(rb.velocity.x, -movespeed);
        }
    }
}
