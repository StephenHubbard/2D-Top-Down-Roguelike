using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using GridPathfindingSystem;

/*
 * Responsible for all Enemy Movement Pathfinding
 * */
public class EnemyPathfindingMovement : MonoBehaviour {


    [SerializeField] private float moveSpeed = 20f;
    private Rigidbody2D myRb;

    private List<Vector3> pathVectorList;
    private int currentPathIndex;
    private Vector3 moveDir;
    private KnockBack knockBack;
    private bool allowedToMove = true;

    private Vector2 lastMoveDir;

    private void Awake() {
        knockBack = GetComponent<KnockBack>();
        myRb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        HandleMovement();
    }

    private void FixedUpdate() {
        if (knockBack.ReturnGettingKnockedBack() && !allowedToMove) { return; }

        myRb.velocity = moveDir * moveSpeed;

        if (lastMoveDir.x < 0) {
            GetComponent<SpriteRenderer>().flipX = true;
        } else {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void AllowedToMoveToggle(bool boolValue) {
        allowedToMove = boolValue;
    }

    private void HandleMovement() {
        if (!allowedToMove) { return; }

        if (moveDir.x < 0) {
            lastMoveDir.x = -1;
        } else {
            lastMoveDir.x = 1;
        }

        if (pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            float reachedTargetDistance = .3f;
            if (Vector3.Distance(GetPosition(), targetPosition) > reachedTargetDistance) {
                moveDir = (targetPosition - GetPosition()).normalized;
            } else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    StopMoving();
                }
            }
        } 
    }

    public void StopMoving() {
        // pathVectorList = null;
        moveDir = Vector3.zero;
    }

    public List<Vector3> GetPathVectorList() {
        return pathVectorList;
    }

    public void MoveTo(Vector3 targetPosition) {
        SetTargetPosition(targetPosition);
    }

    public void SetTargetPosition(Vector3 targetPosition) {
        currentPathIndex = 0;

        pathVectorList = new List<Vector3> { targetPosition };

        if (pathVectorList != null && pathVectorList.Count > 1) {
            pathVectorList.RemoveAt(0);
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    
    public void Enable() {
        enabled = true;
    }

    public void Disable() {
        enabled = false;
        myRb.velocity = Vector3.zero;
    }

}
