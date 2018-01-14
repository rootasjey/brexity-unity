using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextProximity : MonoBehaviour {

    [TextArea(3,10)]
    public string textContent;
    public LayerMask triggerLayer;
    public Canvas canvas;
    public GameObject textContainerTemplate;

    private Text text;
    private Vector3 parentPosition;
    private GameObject textContainer;

    void Start()
    {
        textContainer = Instantiate(textContainerTemplate, gameObject.GetComponentInParent<Transform>().position, gameObject.GetComponentInParent<Transform>().rotation);
        textContainer.transform.SetParent(canvas.transform, false);
        text = textContainer.GetComponentInChildren<Text>();
        text.text = textContent;
        textContainer.SetActive(false);
        Debug.Log("I think i'm instantiate");
        Debug.Log(textContainer.name+" "+ textContainer.activeInHierarchy);
    }

    void FixedUpdate()
    {
        parentPosition = gameObject.GetComponentInParent<Transform>().position;
        //Debug.Log("Parent Position: " + parentPosition);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("I'm ...");
        // Display Label only with specific layer
        if (triggerLayer.value == (triggerLayer.value | (1 << collision.gameObject.layer)))
        {
            Debug.Log("IN");
            Debug.Log(textContainer.name + " " + textContainer.activeInHierarchy);
            textContainer.SetActive(true);
            Debug.Log(textContainer.name + " " + textContainer.activeInHierarchy);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("I'm ...");
        // Dismiss Label only with specific layer
        if (triggerLayer.value == (triggerLayer.value | (1 << collision.gameObject.layer)))
        {
            Debug.Log("OUT");
            Debug.Log(textContainer.name + " " + textContainer.activeInHierarchy);
            textContainer.SetActive(false);
            Debug.Log(textContainer.name+" "+ textContainer.activeInHierarchy);
        }

    }

    void OnGUI()
    {
        Vector3 getPixelPos = Camera.main.WorldToScreenPoint(parentPosition);
        //getPixelPos.y = Screen.height - getPixelPos.y;
        textContainer.transform.position = new Vector3(getPixelPos.x, getPixelPos.y, textContainer.transform.position.z);
        //getPixelPos.x -= width;
        //GUI.Label(new Rect(getPixelPos.x, getPixelPos.y, width, height), text);
    }
}
