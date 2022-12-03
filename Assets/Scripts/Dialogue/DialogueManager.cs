using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : Singleton<DialogueManager>
{

    [SerializeField] public GameObject dialogueBox;
    [SerializeField] private GameObject tasksButtonsContainer;
    public bool justStarted;

    [SerializeField] private int currentLine;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject nameBox;
    private string[] dialogueLines;
    private const string startsWithSignifierString = "n-";

    public void ContinueDialogue()
    {
        if (!justStarted)
        {
        currentLine++;
            if (currentLine >= dialogueLines.Length)
            {
                CloseDialogueBox();
            }
            else
            {
                CheckIfName();
                dialogueText.text = dialogueLines[currentLine];
            }
        }
        else
        {
            justStarted = false;
        }
    }

    public void ShowTaskButtonsContainer(bool isTask) {
        if (isTask) {
            tasksButtonsContainer.SetActive(true);
        } else {
            tasksButtonsContainer.SetActive(false);
        }
    }

    public void CloseDialogueBox() {
        dialogueBox.SetActive(false);
        justStarted = true;
        ActiveWeapon.Instance.ReadingDialogueToggle(false);
    }
    
    // newLines is passed through from the DialogueActivator class that calls this function
    public void ShowDialogue(string[] newLines, bool isPerson) {
        justStarted = true;
        dialogueLines = newLines;
        currentLine = 0;
        CheckIfName();
        dialogueText.text = dialogueLines[currentLine];
        dialogueBox.SetActive(true);
        nameBox.SetActive(isPerson);
        ContinueDialogue();
    }

    // Can signify who's talking in the inspector
    public void CheckIfName() {
        if (dialogueLines[currentLine].StartsWith(startsWithSignifierString)) {
            nameText.text = dialogueLines[currentLine].Replace(startsWithSignifierString, "");
            currentLine++;
        }
    }

}