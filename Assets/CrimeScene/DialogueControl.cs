using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{

    public TMP_Text dialogueOption1;
    public TMP_Text dialogueOption2;
    public TMP_Text dialogueOption3;
    public TMP_Text dialogueResponse;

    public Canvas dialogueCanvas;
    public GameObject questionPanel;
   

    private PlayerInput playerInput;

    public Dialogue currentDialogue;


    // Start is called before the first frame update
    void Start()
    {

        playerInput = FindObjectOfType<PlayerInput>();
        dialogueCanvas = FindObjectOfType<Canvas>();

        var texts = FindObjectsOfType<TMP_Text>();
        if (dialogueOption1 == null)
        {
            dialogueOption1 = Array.Find(texts, text => text.name == "DialogueOption1");
            if (dialogueOption1 == null)
            {
                Debug.Log("Dialogue Option 1 was not set and could not find a named TMP_Text");
            }
            dialogueOption2 = Array.Find(texts, text => text.name == "DialogueOption2");
            if (dialogueOption2 == null)
            {
                Debug.Log("Dialogue Option 2 was not set and could not find a named TMP_Text");
            }
            dialogueOption3 = Array.Find(texts, text => text.name == "DialogueOption3");
            if (dialogueOption3 == null)
            {
                Debug.Log("Dialogue Option 3 was not set and could not find a named TMP_Text");
            }
            dialogueResponse = Array.Find(texts, text => text.name == "DialogueResponse");
            if (dialogueResponse == null)
            {
                Debug.Log("Dialogue Option 3 was not set and could not find a named TMP_Text");
            }
        }

        dialogueCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateDialogue(in Dialogue dialogue){

        dialogueCanvas.enabled = true;
        currentDialogue = dialogue;

        toggleButtonPanel(true);

        dialogueOption1.SetText(currentDialogue.question1);
        dialogueOption2.SetText(currentDialogue.question2);
        dialogueOption3.SetText(currentDialogue.question3);


    }

    public void Question1Asked()
    {
        dialogueOption1.SetText("");
        dialogueOption2.SetText("");
        dialogueOption3.SetText("");
        dialogueResponse.SetText(currentDialogue.response1);
        toggleButtonPanel(false);
    }

    public void Question2Asked()
    {
        dialogueOption1.SetText("");
        dialogueOption2.SetText("");
        dialogueOption3.SetText("");
        dialogueResponse.SetText(currentDialogue.response2);
        toggleButtonPanel(false);
    }

    public void Question3Asked()
    {
        dialogueOption1.SetText("");
        dialogueOption2.SetText("");
        dialogueOption3.SetText("");
        dialogueResponse.SetText(currentDialogue.response3);
        toggleButtonPanel(false);
    }


    public void CloseDialogue()
    {
        dialogueOption1.SetText("");
        dialogueOption2.SetText("");
        dialogueOption3.SetText("");
        playerInput.SwitchCurrentActionMap("Player");
        dialogueCanvas.enabled = false;
        toggleButtonPanel(false);

    }

    private void toggleButtonPanel(bool enable)
    {
        var btns = questionPanel.GetComponentsInChildren<Button>(true);
        Debug.Log("Toggling " + btns.Length + " buttons to " + enable);
        foreach (Button btn in btns)
        {
            btn.gameObject.SetActive(enable);
        }
    }
}
