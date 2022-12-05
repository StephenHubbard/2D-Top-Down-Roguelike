using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskGoal
{
    public enum GoalType {
        Gather, 
        Kill, 
        Discover, 
        TalkTo
    }

    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public Pickup.PickupType pickupType;
    public EnemyHealth.EnemyType enemyType;


    public bool IsReached() {
        return (currentAmount >= requiredAmount);
    }

    public void ItemGathered(Pickup.PickupType pickupType) {
        if (goalType == GoalType.Gather && this.pickupType == pickupType) {
            currentAmount++;
        }
    }

    public void EnemyKilled(EnemyHealth.EnemyType enemyType) {
        if (goalType == GoalType.Kill && this.enemyType == enemyType) {
            currentAmount++;
        }
    }
}
