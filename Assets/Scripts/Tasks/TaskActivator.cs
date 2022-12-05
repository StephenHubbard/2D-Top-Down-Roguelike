using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TaskActivator : MonoBehaviour
{   
    [SerializeField] public Task task;
    [SerializeField] private GameObject exclamationPointIcon;
    [SerializeField] private GameObject questionMarkIcon;
    [SerializeField] private MonoBehaviour actionToTakeAfterTaskCompleted;

    private DialogueActivator dialogueActivator;

    private void Awake() {
        dialogueActivator = GetComponent<DialogueActivator>();
    }

    private void Start() {
        task.OnTaskComplete += CompleteTask;
    }

    public void TaskIconOff() {
        if (exclamationPointIcon && questionMarkIcon) {
            exclamationPointIcon.SetActive(false);
            questionMarkIcon.SetActive(false);
        }
    }

    public void QuestionMarkActive() {
        if (questionMarkIcon) {
            questionMarkIcon.SetActive(true);
        }
    }

    public void CompleteTask() {
        foreach (var taskList in FindObjectsOfType<TaskList>())
        {
            taskList.CheckIfComplete();
        }

        QuestionMarkActive();
        dialogueActivator.CheckDialogueState();
    }

    public void TurnInTask() {
        task.TurnInTask();
        TaskIconOff();
        dialogueActivator.CheckDialogueState();
        ActionToTake();
    }

    public void ActionToTake() {
        if (actionToTakeAfterTaskCompleted) {
            (actionToTakeAfterTaskCompleted as ITaskComplete).TaskComplete();
        }
    }
}
