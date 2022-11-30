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
        Attacking,
    }

    public float enemyKnockBackThrust = 15f;
    public int damageDoneToHero = 1;

    [SerializeField] private float targetChaseRange = 5f;
    [SerializeField] private float attackRange;
    [SerializeField] private float stopChaseDistance = 10f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 2f;

    private EnemyPathfindingMovement pathfindingMovement;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private State state;
    private bool canAttack = true;
    private float timeRoaming = 0f;
    private PlayerHealth playerHealth;

    private void Awake() {
        pathfindingMovement = GetComponent<EnemyPathfindingMovement>();
        state = State.Roaming;
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Start() {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }
   

    private void Update() {
        MovementStateControl();
    }

    private void MovementStateControl() {
        switch (state)
        {
        default:
        case State.Roaming: 

            timeRoaming += Time.deltaTime;

            pathfindingMovement.MoveTo(roamPosition);

            float reachedPositionDistance = 1f;
            if ((Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance) || timeRoaming > 4f) {
                roamPosition = GetRoamingPosition();
            }
            FindTarget();
            break;

        case State.ChaseTarget:

            if (Vector3.Distance(transform.position, PlayerController.Instance.GetPosition()) < attackRange) {
                state = State.Attacking;
            }

            pathfindingMovement.MoveTo(PlayerController.Instance.GetPosition());

            if (Vector3.Distance(transform.position, PlayerController.Instance.GetPosition()) > stopChaseDistance) {
                state = State.GoingBackToStart;
            }
            break;

        case State.GoingBackToStart:

            pathfindingMovement.MoveTo(startingPosition);

            reachedPositionDistance = 1f;
            if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance) {
                state = State.Roaming;
                StartCoroutine(RoamingCo());

            }
            break;

        case State.Attacking:
            if (attackRange != 0 && canAttack && !playerHealth.isDead) {
                GetComponent<EnemyPathfindingMovement>().AllowedToMoveToggle(false);
                (enemyType as IEnemy).Attack();
                canAttack = false;
                StartCoroutine(AttackCooldownCo());
                pathfindingMovement.StopMoving();
            } else {
                state = State.ChaseTarget;
            }

            break;
        }

    }

    private IEnumerator AttackCooldownCo() {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private IEnumerator RoamingCo() {
        while (state == State.Roaming)
        {
            yield return new WaitForSeconds(4f);
            roamPosition = GetRoamingPosition();
        }
    }

    private Vector3 GetRoamingPosition() {
        return startingPosition + UtilsClass.GetRandomDir() * Random.Range(1f, 5f);
    }

    public void FindTarget() {
        timeRoaming = 0f;
        if (Vector3.Distance(transform.position, PlayerController.Instance.GetPosition()) < targetChaseRange && 
            Vector3.Distance(transform.position, PlayerController.Instance.GetPosition()) > attackRange) {
            state = State.ChaseTarget;
        } 
    }

    
}
