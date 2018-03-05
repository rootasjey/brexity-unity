using UnityEngine;

public class TraderTracking : MonoBehaviour {
    public float timeBeforeTrigger;

    public GameObject trader;

    // Use this for initialization
    void Start () {
        timeBeforeTrigger = new System.Random().Next(20, 60);
	}
	
	// Update is called once per frame
	void Update () {
        timeBeforeTrigger -= Time.deltaTime;

        if (timeBeforeTrigger <= 0) {
            var o = Instantiate(trader, new Vector3(transform.position.x, 15), new Quaternion());
            o.layer = 14;

            timeBeforeTrigger = new System.Random().Next(60, 120);
        }
    }
}
