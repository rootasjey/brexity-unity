using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public string continueText;
    public string closeText;

    public Text nameText;
    public Text dialogueText;
    public Image dialogueImage;
    public Button dialogueContinueButton;
    public GameObject dialogueBox;

    private Queue<string> sentences;

    // Use this for initialization
    void Start () {
        sentences = new Queue<string>();
	}

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBox.SetActive(true);
        dialogueAppear(false, 0f);
        dialogueAppear(true, 0.5f);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach( string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentences();
        dialogueImage.CrossFadeAlpha(1f, 1f, true);
    }

    public void DisplayNextSentences()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void OnGUI()
    {
        Debug.Log(sentences.Count);
        if (sentences.Count == 0)
        {
            dialogueContinueButton.GetComponentInChildren<Text>().text = closeText;
        }
        else if (sentences.Count > 0)
        {
            dialogueContinueButton.GetComponentInChildren<Text>().text = continueText;
        }
    }

    void EndDialogue()
    {
        dialogueAppear(false, 0.5f);
        dialogueBox.SetActive(false);
    }

    void dialogueAppear(bool appear, float fadeTime)
    {
        if(appear)
        {
            nameText.CrossFadeAlpha(1f, fadeTime, true);
            dialogueText.CrossFadeAlpha(1f, fadeTime, true);
            dialogueImage.CrossFadeAlpha(1f, fadeTime, true);
            dialogueContinueButton.GetComponent<Image>().CrossFadeAlpha(1f, fadeTime, true);
            dialogueContinueButton.GetComponentInChildren<Text>().CrossFadeAlpha(1f, fadeTime, true);
        }
        else
        {
            nameText.CrossFadeAlpha(0f, fadeTime, true);
            dialogueText.CrossFadeAlpha(0f, fadeTime, true);
            dialogueImage.CrossFadeAlpha(0f, fadeTime, true);
            dialogueContinueButton.GetComponent<Image>().CrossFadeAlpha(0f, fadeTime, true);
            dialogueContinueButton.GetComponentInChildren<Text>().CrossFadeAlpha(0f, fadeTime, true);
        }
    }
}
