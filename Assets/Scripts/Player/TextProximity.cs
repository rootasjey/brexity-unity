using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextProximity : MonoBehaviour {

    [TextArea(3,10)]
    public string text;
    public LayerMask triggerLayer;

    private bool toDisplay = false;
    private Vector3 parentPosition;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Display Label only with specific layer
        if (triggerLayer.value == (triggerLayer.value | (1 << collision.gameObject.layer)))
        {
            parentPosition = gameObject.GetComponentInParent<Transform>().position;
            toDisplay = true;
        }
    }

    void OnUpdate()
    {
        parentPosition = gameObject.GetComponentInParent<Transform>().position;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (triggerLayer.value == (triggerLayer.value | (1 << collision.gameObject.layer)))
        {
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Dismiss Label only with specific layer
        if (triggerLayer.value == (triggerLayer.value | (1 << collision.gameObject.layer)))
        {
            toDisplay = false;
        }

    }

    void OnGUI()
    {
        if (toDisplay)
        {
            float width = 200f;
            float height = 100f;
            //Vector3 size = GetComponent<BoxCollider2D>().bounds.size;
            Vector3 getPixelPos = Camera.main.WorldToScreenPoint(parentPosition);
            getPixelPos.y = Screen.height - getPixelPos.y;
            getPixelPos.x -= width;
            
            //GUI.Label(new Rect(getPixelPos.x, getPixelPos.y, width, height), text);
        }
    }
}
