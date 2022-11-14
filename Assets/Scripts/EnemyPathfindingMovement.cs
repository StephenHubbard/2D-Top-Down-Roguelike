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
    private Vector3 lastMoveDir;
    private KnockBack knockBack;

    private void Awake() {
        knockBack = GetComponent<KnockBack>();
        myRb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        HandleMovement();
    }

    private void FixedUpdate() {
        if (knockBack.ReturnGettingKnockedBack()) { return; }

        myRb.velocity = moveDir * moveSpeed;

        if (moveDir.x < 0) {
            GetComponent<SpriteRenderer>().flipX = true;
        } else {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void HandleMovement() {
        PrintPathfindingPath();
        if (pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            float reachedTargetDistance = 1f;
            if (Vector3.Distance(GetPosition(), targetPosition) > reachedTargetDistance) {
                moveDir = (targetPosition - GetPosition()).normalized;
                lastMoveDir = moveDir;
            } else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    StopMoving();
                }
            }
        } 
    }

    public void StopMoving() {
        pathVectorList = null;
        moveDir = Vector3.zero;
    }

    public List<Vector3> GetPathVectorList() {
        return pathVectorList;
    }

    private void PrintPathfindingPath() {
        if (pathVectorList != null) {
            for (int i=0; i<pathVectorList.Count - 1; i++) {
                Debug.DrawLine(pathVectorList[i], pathVectorList[i + 1]);
            }
        }
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

    public Vector3 GetLastMoveDir() {
        return lastMoveDir;
    }      
    
    public void Enable() {
        enabled = true;
    }

    public void Disable() {
        enabled = false;
        myRb.velocity = Vector3.zero;
    }

}
