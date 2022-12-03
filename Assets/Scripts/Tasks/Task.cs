using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Task
{
    public bool isActive;
    public bool isComplete = false;
    public string title;

    public TaskGoal taskGoal;
    public TaskList taskList;

    public event EventHandler OnTaskCompleted;

    private void Start() {
        OnTaskCompleted += TaskCleanup;
    }

    public void TaskComplete() {
        OnTaskCompleted?.Invoke(this, EventArgs.Empty);
    }

    public void TaskCleanup(object sender, EventArgs e) {
        Debug.Log("hit");
        isActive = false;
        isComplete = true;
        taskList.CompleteTask();
    }
}
