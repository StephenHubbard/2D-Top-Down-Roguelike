using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskList : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text taskGoalText;
    [SerializeField] private GameObject checkMark;

    public Task task;

    private void OnEnable() {
        CheckIfComplete();
    }

    public void CheckIfComplete() {
        foreach (var task in TaskManager.Instance.ReturnAllActiveTasks())
        {
            if (this.task.title == task.title) {
                if (task.taskGoal.IsReached()) {
                    checkMark.SetActive(true);
                }
            }
        }

        foreach (var task in TaskManager.Instance.ReturnAllActiveTasks())
        {
            if (this.task.title == task.title) {
                if (task.state == Task.State.TurnedIn) {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void Update() {
        titleText.text = task.title;
        taskGoalText.text = task.taskGoal.currentAmount.ToString() + "/" + task.taskGoal.requiredAmount.ToString();
    }

    public void SetTask(Task taskToSet) {
        task = taskToSet;
    }

    public void CompleteTask() {
        checkMark.SetActive(true);
    }
}
