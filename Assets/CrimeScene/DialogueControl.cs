using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{

    public TMP_Text dialogueOption1;
    public TMP_Text dialogueOption2;
    public TMP_Text dialogueOption3;
    public TMP_Text dialoguePrefix;
    public TMP_Text dialogueResponse;
    public TMP_Text dialogueName;

    public Canvas dialogueCanvas;
    public GameObject questionPanel;
    public GameObject responsePanel;


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
            dialoguePrefix = Array.Find(texts, text => text.name == "PrefixText");
            if (dialogueResponse == null)
            {
                Debug.Log("Dialogue Option 3 was not set and could not find a named TMP_Text");
            }
            dialogueName = Array.Find(texts, text => text.name == "Name");
            if (dialogueResponse == null)
            {
                Debug.Log("Dialogue Option 3 was not set and could not find a named TMP_Text");
            }
        }

        dialogueCanvas.gameObject.SetActive(false); ;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateDialogue(in Dialogue dialogue){

        dialogueCanvas.gameObject.SetActive(true); ;
        currentDialogue = dialogue;

        toggleGroup(responsePanel, true);
        toggleGroup(questionPanel, false);
        dialogueResponse.SetText(currentDialogue.introduction);

        dialoguePrefix.SetText(currentDialogue.questionPrefix);
        dialogueOption1.SetText(currentDialogue.question1);
        dialogueOption2.SetText(currentDialogue.question2);
        dialogueOption3.SetText(currentDialogue.question3);
        dialogueName.SetText(currentDialogue.suspectName);


    }

    public void goToQuestions()
    {
        dialogueName.SetText("Detective");
        toggleGroup(questionPanel, true);
        toggleGroup(responsePanel, false);
    }

    public void Question1Asked()
    {
        dialogueResponse.SetText(currentDialogue.response1);
        toggleGroup(questionPanel, false);
        toggleGroup(responsePanel, true);
        dialogueName.SetText(currentDialogue.suspectName);

        if (currentDialogue.evidenceResponseNumber == 1)
        {
            PersistenceSingleton.Instance.AddNewItem(currentDialogue.reward);
        }
    }

    public void Question2Asked()
    {
        dialogueResponse.SetText(currentDialogue.response2);
        toggleGroup(questionPanel, false);
        toggleGroup(responsePanel, true);
        dialogueName.SetText(currentDialogue.suspectName);

        if (currentDialogue.evidenceResponseNumber == 2)
        {
            PersistenceSingleton.Instance.AddNewItem(currentDialogue.reward);
        }
    }

    public void Question3Asked()
    {
        dialogueResponse.SetText(currentDialogue.response3);
        toggleGroup(questionPanel, false);
        toggleGroup(responsePanel, true);
        dialogueName.SetText(currentDialogue.suspectName);

        if (currentDialogue.evidenceResponseNumber == 3)
        {
            PersistenceSingleton.Instance.AddNewItem(currentDialogue.reward);
        }
    }


    public void CloseDialogue()
    {
        dialogueOption1.SetText("");
        dialogueOption2.SetText("");
        dialogueOption3.SetText("");
        playerInput.SwitchCurrentActionMap("Player");
        dialogueCanvas.gameObject.SetActive(false); ;

    }

    private void toggleGroup(GameObject group, bool enable)
    {
        var buttonPanelThings = group.GetComponentsInChildren<Button>(true);
        foreach (Button btn in buttonPanelThings)
        {
            btn.gameObject.SetActive(enable);
        }
        var textThings = group.GetComponentsInChildren<TMP_Text>(true);
        foreach (TMP_Text btn in textThings)
        {
            btn.gameObject.SetActive(enable);
        }
    }
}
