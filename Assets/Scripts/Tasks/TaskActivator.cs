using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskActivator : MonoBehaviour
{   
    [SerializeField] private Task task;
    [SerializeField] private GameObject exclamationPointTaskIcon;
    [SerializeField] private GameObject questionMarkTaskIcon;

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
        exclamationPointTaskIcon.SetActive(false);
    }

    public Task ReturnTask() {
        return task;
    }

    public void ViewingTask() {
        TaskManager.Instance.PotentialTaskToAdd(task, this);
    }
}
