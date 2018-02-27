using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneRunControllerScript : MonoBehaviour {

    public float speed = 0.05f;

    [Range(-1, 1)]
    public int direction = 1;

    public float droneTimer = 2f;
    public float pauseTimer = 2f;

    private float timer;
    // Use this for initialization
    void OnEnable()
    {
        direction = 1;
        StartCoroutine(coroutineUpDown());
    }
    
    IEnumerator coroutineUpDown()
    {
        timer = droneTimer;

        while (timer > 0)
        {
            yield return new WaitForSeconds(0.03f);
            transform.position = transform.position + new Vector3(0, direction * speed, 0);
        }
        yield return StartCoroutine(coroutinePause());
        StopCoroutine(coroutineUpDown());
    }

    IEnumerator coroutinePause()
    {
        timer = pauseTimer;
        while (timer > 0)
        {
            yield return new WaitForSeconds(0.5f);
        }
        direction *= -1;
        yield return StartCoroutine(coroutineUpDown());
        StopCoroutine(coroutinePause());
    }

    public void FixedUpdate()
    {
        timer = (timer <= 0) ? 0 : timer - 1 * Time.fixedDeltaTime;
    }
}

