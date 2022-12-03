using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
    public bool isActive;
    public bool isComplete = false;
    public string title;

    public TaskGoal taskGoal;
    public TaskList taskList;

    public void TaskComplete() {
        isActive = false;
        isComplete = true;
        taskList.CompleteTask();
    }
}
