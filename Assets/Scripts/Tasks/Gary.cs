using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gary : MonoBehaviour, ITaskComplete
{
    [SerializeField] private GameObject boulder;

    private void Start() {
        if (GetComponent<TaskActivator>().task.state == Task.State.TurnedIn) {
            TaskComplete();
        }
    }

    public void TaskComplete()
    {
        Destroy(boulder);
    }
}
