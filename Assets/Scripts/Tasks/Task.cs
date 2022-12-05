using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Task
{
    public enum State {
        Inactive, 
        Active, 
        Complete,
        TurnedIn,
    }

    public State state;

    public string title;
    public TaskGoal taskGoal;

    public Action OnTaskComplete;

    public void CheckIfActive() {
        foreach (var task in TaskManager.Instance.ReturnAllActiveTasks())
        {
            if (this.title == task.title) {
                state = State.Active;
            }
        }
    }

    public void TaskGoalCompleted() {
        state = State.Complete;
        OnTaskComplete?.Invoke();
    }

    public void TurnInTask() {
        state = State.TurnedIn;
    }

    
}
