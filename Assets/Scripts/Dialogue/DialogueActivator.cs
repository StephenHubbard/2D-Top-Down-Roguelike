using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Put on gameobjects that can be toggled with opening the dialogue window (currently spacebar).  If isPerson isn't toggled true, the name box window will not appear.
public class DialogueActivator : MonoBehaviour
{
    [SerializeField] private string[] lines;
    [SerializeField] private bool showNameBox;
    [SerializeField] private bool isTask;
    [SerializeField] private string audioStringReference;
    [SerializeField] private GameObject dialogueBubble;

    private bool canActivate;

    public void HideDialogueBubble() {
        dialogueBubble.SetActive(false);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            OpenDialogue();
        }
    }

    private void OpenDialogue() {
        if (GetComponent<TaskActivator>() && (GetComponent<TaskActivator>().ReturnTask().isComplete || GetComponent<TaskActivator>().ReturnTask().isActive)) { return; }

        if (canActivate) {
            if(!DialogueManager.Instance.dialogueBox.activeInHierarchy) {
                if (audioStringReference != null) {
                    AudioManager.Instance.Play(audioStringReference);
                }
                DialogueManager.Instance.ShowDialogue(lines, showNameBox);
                if (isTask) {
                    DialogueManager.Instance.ShowTaskButtonsContainer(isTask);
                    GetComponent<TaskActivator>().ViewingTask();
                }
                ActiveWeapon.Instance.ReadingDialogueToggle(true);
            } else {
                DialogueManager.Instance.ContinueDialogue();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (GetComponent<TaskActivator>() && (GetComponent<TaskActivator>().ReturnTask().isComplete || GetComponent<TaskActivator>().ReturnTask().isActive)) { return; }

        if(other.GetComponent<PlayerController>()) {
            dialogueBubble.SetActive(true);
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.GetComponent<PlayerController>()) {
            DialogueManager.Instance.CloseDialogueBox();
            dialogueBubble.SetActive(false);
            canActivate = false;
        }
    }
}
