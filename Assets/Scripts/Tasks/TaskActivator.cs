using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TaskActivator : MonoBehaviour
{   
    [SerializeField] public Task task;
    [SerializeField] private MonoBehaviour actionToTakeAfterTaskCompleted;

    private GameObject exclamationPointIcon;
    private GameObject questionMarkIcon;
    private DialogueActivator dialogueActivator;

    private void Awake() {
        dialogueActivator = GetComponent<DialogueActivator>();
        SetIcons();
    }

    private void Start() {
        DetectTaskState();

        if (task.state == Task.State.TurnedIn) {
            ActionToTake();
        }
    }

    private void SetIcons() {
        exclamationPointIcon = transform.GetChild(0).gameObject;
        questionMarkIcon = transform.GetChild(1).gameObject;
    }

    private void DetectTaskState() {
        foreach (var task in TaskManager.Instance.ReturnAllActiveTasks())
        {
            if (this.task.title == task.title) {
                task.OnTaskComplete += CompleteTask;
                return;
            } 
        } 

        task.OnTaskComplete += CompleteTask;
    }

    public void TaskIconOff() {
        if (exclamationPointIcon && questionMarkIcon) {
            exclamationPointIcon.SetActive(false);
            questionMarkIcon.SetActive(false);
        }
    }

    public void ExclamationPointIconActive() {
        if (exclamationPointIcon) {
            exclamationPointIcon.SetActive(true);
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
