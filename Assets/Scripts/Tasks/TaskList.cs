using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskList : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text taskGoalText;
    [SerializeField] private GameObject checkMark;

    private Task task;

    private void Update() {
        titleText.text = task.title;
        taskGoalText.text = task.taskGoal.currentAmount.ToString() + "/" + task.taskGoal.requiredAmount.ToString();
    }

    public void SetTask(Task task) {
        this.task = task;
    }

    public void CompleteTask() {
        checkMark.SetActive(true);
    }
}
