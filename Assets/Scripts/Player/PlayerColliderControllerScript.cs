using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderControllerScript : MonoBehaviour {

    public CapsuleCollider2D stand;
    public CapsuleCollider2D crouch;

    private PlayerCrouchControllerScript playerCrouch;
    private bool isCrouched = false;

    // Use this for initialization
    void Start () {
        playerCrouch = GetComponent<PlayerCrouchControllerScript>();
        stand.enabled = true;
        crouch.enabled = false;
    }

    void FixedUpdate () {

        isCrouched = playerCrouch.getCrouched();

        if (isCrouched)
        {
            stand.enabled = false;
            crouch.enabled = true;
        }
        else
        {
            stand.enabled = true;
            crouch.enabled = false;
        }
    }
}
