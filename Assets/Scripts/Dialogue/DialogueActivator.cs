using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Put on gameobjects that can be toggled with opening the dialogue window (currently spacebar).  If isPerson isn't toggled true, the name box window will not appear.
public class DialogueActivator : MonoBehaviour
{

    public string[] lines;
    public bool isPerson;

    [SerializeField] private string audioStringReference;
    [SerializeField] private GameObject dialogueBubble;

    private bool canActivate;

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            OpenDialogue();
        }
    }

    private void OpenDialogue() {
        if (canActivate) {
            if(!DialogueManager.Instance.dialogueBox.activeInHierarchy) {
                if (audioStringReference != null) {
                    AudioManager.Instance.Play(audioStringReference);
                }
                DialogueManager.Instance.ShowDialogue(lines, isPerson);
            } else {
                DialogueManager.Instance.ContinueDialogue();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
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
