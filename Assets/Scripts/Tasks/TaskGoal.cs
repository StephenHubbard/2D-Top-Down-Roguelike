using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskGoal
{
    public enum GoalType {
        Gather, 
        Kill, 
        Contact, 
        Travel
    }

    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public Pickup.PickupType pickupType;

    public bool IsReached() {
        return (currentAmount >= requiredAmount);
    }

    public void ItemGathered(Pickup.PickupType pickupType) {
        if (goalType == GoalType.Gather && this.pickupType == pickupType) {
            currentAmount++;
        }
    }
}
