using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class EnemyAI : MonoBehaviour
{
    private enum State {
        Roaming, 
        ChaseTarget,
        GoingBackToStart,
    }

    public float enemyKnockBackThrust = 15f;
    public int damageDoneToHero = 1;

    [SerializeField] private float targetChaseRange = 5f;

    private EnemyPathfindingMovement pathfindingMovement;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private State state;

    private void Awake() {
        pathfindingMovement = GetComponent<EnemyPathfindingMovement>();
        state = State.Roaming;
    }

    private void Start() {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }
   

    private void Update() {
        switch (state)
        {
        default:
        case State.Roaming: 

            pathfindingMovement.MoveTo(roamPosition);

            float reachedPositionDistance = 1f;
            if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance) {
                roamPosition = GetRoamingPosition();
            }
            FindTarget();
            break;

        case State.ChaseTarget:
            pathfindingMovement.MoveTo(PlayerController.instance.GetPosition());
            float stopChaseDistance = 12f;
            if (Vector3.Distance(transform.position, PlayerController.instance.GetPosition()) > stopChaseDistance) {
                state = State.GoingBackToStart;
            }
            break;

        case State.GoingBackToStart:
            pathfindingMovement.MoveTo(startingPosition);

            reachedPositionDistance = 1f;
            if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance) {
                state = State.Roaming;
            }
            break;
        }
    }

    private Vector3 GetRoamingPosition() {
        return startingPosition + UtilsClass.GetRandomDir() * Random.Range(1f, 5f);
    }

    private void FindTarget() {
        if (Vector3.Distance(transform.position, PlayerController.instance.GetPosition()) < targetChaseRange) {
            state = State.ChaseTarget;
        }
    }

    
}
