using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderControllerScript : MonoBehaviour {

    public BoxCollider2D stand;
    public BoxCollider2D crouch;
    public CircleCollider2D circle;

    private PlayerCrouchControllerScript playerCrouch;
    private bool isCrouched = false;

    // Use this for initialization
    void Start () {
        playerCrouch = GetComponent<PlayerCrouchControllerScript>();
        stand.enabled = true;
        crouch.enabled = false;
        circle.enabled = true;
    }

    void FixedUpdate () {

        isCrouched = playerCrouch.getCrouched();

        if (isCrouched)
        {
            stand.enabled = false;
            crouch.enabled = true;
            circle.enabled = true;
        }
        else
        {
            stand.enabled = true;
            crouch.enabled = false;
            circle.enabled = true;
        }
    }
}
