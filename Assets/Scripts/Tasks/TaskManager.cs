using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : Singleton<TaskManager>
{
    [SerializeField] private List<Task> allTasks = new List<Task>();
    [SerializeField] private GameObject currentTaskContainer;
    [SerializeField] private GameObject taskListPrefab;

    private Task potentialTaskToAdd;
    private TaskActivator TaskActivator;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            ShowCurrentTasks();
        }
    }

    public void ShowCurrentTasks() {
        currentTaskContainer.SetActive(!currentTaskContainer.activeInHierarchy);
    }

    public List<Task> ReturnAllActiveTasks() {
        return allTasks;
    }

    public void AcceptButton() {
        AddActiveTask();
        DialogueManager.Instance.ShowTaskButtonsContainer(false);
        CreateNewTaskList();
    }

    public void DeclineButton() {
        DialogueManager.Instance.CloseDialogueBox();
        DialogueManager.Instance.ShowTaskButtonsContainer(false);
    }

    public void AddActiveTask() {
        allTasks.Add(potentialTaskToAdd);
        TaskActivator.ActiveTask();
        DialogueManager.Instance.CloseDialogueBox();
        TaskActivator.GetComponent<DialogueActivator>().HideDialogueBubble();
    }

    public void PotentialTaskToAdd(Task Task, TaskActivator TaskActivator) {
        this.TaskActivator = TaskActivator;
        this.potentialTaskToAdd = Task;
    }

    private void CreateNewTaskList() {
        GameObject newTaskList = Instantiate(taskListPrefab, currentTaskContainer.transform.position, transform.rotation);
        newTaskList.transform.SetParent(currentTaskContainer.transform);
        newTaskList.GetComponent<TaskList>().SetTask(potentialTaskToAdd);
        potentialTaskToAdd.taskList = newTaskList.GetComponent<TaskList>();
    }
}
