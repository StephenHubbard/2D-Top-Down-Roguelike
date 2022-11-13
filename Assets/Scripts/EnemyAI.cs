using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class EnemyAI : MonoBehaviour
{
    private EnemyPathfindingMovement pathfindingMovement;
    private Vector3 startingPosition;
    private Vector3 roamPosition;

    private void Awake() {
        pathfindingMovement = GetComponent<EnemyPathfindingMovement>();
    }

    private void Start() {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    private void Update() {
        pathfindingMovement.MoveTo(roamPosition);

        float reachedPositionDistance = 1f;
        if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance) {
            roamPosition = GetRoamingPosition();
        }
    }

    private Vector3 GetRoamingPosition() {
        return startingPosition + UtilsClass.GetRandomDir() * Random.Range(1f, 5f);
    }

    
}
