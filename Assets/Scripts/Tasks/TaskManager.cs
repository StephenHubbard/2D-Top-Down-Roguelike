using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : Singleton<TaskManager>
{
    [SerializeField] private GameObject taskListPrefab;
    [SerializeField] private GameObject activeQuestsContainer;

    public List<Task> allTasks = new List<Task>();
    private int currentAmountOfTasks = 0;

    private Task currentTask;
    private TaskActivator currentTaskActivator;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            activeQuestsContainer.SetActive(!activeQuestsContainer.activeInHierarchy);
        }   
    }

    public void SetPotentialTask(Task task, TaskActivator taskActivator) {
        currentTask = task;
        currentTaskActivator = taskActivator;
    }

    public void AcceptButton() {
        if (currentAmountOfTasks < 4) {
            DialogueManager.Instance.CloseDialogueBox();
            AddTaskToActiveTasks();
        }

        currentTask.CheckIfActive();
        currentTaskActivator.GetComponent<DialogueActivator>().CheckDialogueState();
        currentTaskActivator.TaskIconOff();
    }

    public void DeclineButton() {
        DialogueManager.Instance.CloseDialogueBox();
    }

    public void CompleteTaskButton() {
        foreach (var task in allTasks)
        {
            if (task.title == currentTaskActivator.task.title) {
                task.state = Task.State.TurnedIn;
            }
        }

        DialogueManager.Instance.CloseDialogueBox();
        currentTaskActivator.TurnInTask();

        TaskList[] allTaskLists = FindObjectsOfType<TaskList>();

        foreach (var taskList in allTaskLists)
        {
            taskList.CheckIfComplete();
        }

    }

    public List<Task> ReturnAllActiveTasks() {
        return allTasks;
    }

    private void AddTaskToActiveTasks() {
        allTasks.Add(currentTask);
        TaskList newTaskList = Instantiate(taskListPrefab, activeQuestsContainer.transform.position, transform.rotation).GetComponent<TaskList>();
        newTaskList.transform.SetParent(activeQuestsContainer.transform);
        newTaskList.SetTask(currentTask);
        currentAmountOfTasks++;
    }

    public void CompleteTask(Task completedTask) {
        foreach (var task in allTasks)
        {
            if (task.title == completedTask.title) {
                allTasks.Remove(completedTask);
            }
        }
    }
}
