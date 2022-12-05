using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    public enum DialogueState {
        NoTask,
        PreTask,
        DuringTask,
        TaskComplete,
        PostTask,
    }

    public DialogueState dialogueState;

    [SerializeField] private string[] noTaskLines;
    [SerializeField] private string[] preTaskLines;
    [SerializeField] private string[] duringTaskLines;
    [SerializeField] private string[] taskCompleteLine;
    [SerializeField] private string[] postTaskLines;
    [SerializeField] private bool showNameBox;
    [SerializeField] private string audioStringReference;
    [SerializeField] private GameObject dialogueBubble;

    private bool canActivate;
    private TaskActivator taskActivator;

    private void Awake() {
        if (GetComponent<TaskActivator>()) {
            taskActivator = GetComponent<TaskActivator>();
        }
    }

    private void Start() {
        if (GetComponent<TaskActivator>()) {
            CheckDialogueState();
        }
    }

    public void HideDialogueBubble() {
        dialogueBubble.SetActive(false);
    }

    public void ShowDialogueBubble() {
        dialogueBubble.SetActive(true);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            OpenDialogue();
        }
    }

    private void OpenDialogue() {
        if (canActivate) {
            if(!DialogueManager.Instance.dialogueBox.activeInHierarchy) {
                ActiveWeapon.Instance.ReadingDialogueToggle(true);
                WhichLinesToDisplay();
            } else {
                DialogueManager.Instance.ContinueDialogue();
            }
        }
    }

    public void CheckDialogueState() {
        foreach (var task in TaskManager.Instance.ReturnAllActiveTasks())
        {
            if (task.title == taskActivator.task.title) {
                taskActivator.task.state = task.state;
                break;
            }
        }

        if (taskActivator.task.state == Task.State.Inactive) {
            dialogueState = DialogueState.PreTask;
        }

        if (taskActivator.task.state == Task.State.Active) {
            dialogueState = DialogueState.DuringTask;
        }

        if (taskActivator.task.state == Task.State.Complete) {
            dialogueState = DialogueState.TaskComplete;
            taskActivator.TaskIconOff();
            taskActivator.QuestionMarkActive();
        }

        if (taskActivator.task.state == Task.State.TurnedIn) {
            dialogueState = DialogueState.PostTask;
            taskActivator.TaskIconOff();
        }


    }

    private void WhichLinesToDisplay() {
        if (GetComponent<TaskActivator>()) {
            TaskManager.Instance.SetPotentialTask(taskActivator.task, taskActivator);
        }

        switch (dialogueState)
        {
            default:
            case DialogueState.NoTask:
                DialogueManager.Instance.ShowDialogue(noTaskLines, showNameBox);
                break;

            case DialogueState.PreTask:
                DialogueManager.Instance.ShowDialogue(preTaskLines, showNameBox);
                break;

            case DialogueState.DuringTask:
                DialogueManager.Instance.ShowDialogue(duringTaskLines, showNameBox);
                break;

            case DialogueState.TaskComplete:
                DialogueManager.Instance.ShowDialogue(taskCompleteLine, showNameBox);
                break;

            case DialogueState.PostTask:
                DialogueManager.Instance.ShowDialogue(postTaskLines, showNameBox);
                break;
        }

        if (dialogueState == DialogueState.PreTask) {
            DialogueManager.Instance.ShowTaskButtonsContainer(true);
        } else {
            DialogueManager.Instance.ShowTaskButtonsContainer(false);
        }

        if (dialogueState == DialogueState.TaskComplete) {
            DialogueManager.Instance.ShowCompleteTaskButton();
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
