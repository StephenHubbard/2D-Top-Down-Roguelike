using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskActivator : MonoBehaviour
{   
    [SerializeField] private Task task;
    [SerializeField] private GameObject exclamationPoint;

    private TaskManager taskManager;

    private void Awake() {
        taskManager = FindObjectOfType<TaskManager>();
    }

    private void Start() {
        foreach (var task in taskManager.ReturnAllActiveTasks())
        {
            if (this.task.title == task.title) {
                ActiveTask();
            }
        }
    }

    public void ActiveTask() {
        task.isActive = true;
        exclamationPoint.SetActive(false);
    }

    public Task ReturnTask() {
        return task;
    }

    public void ViewingTask() {
        TaskManager.Instance.PotentialTaskToAdd(task, this);
    }
}
