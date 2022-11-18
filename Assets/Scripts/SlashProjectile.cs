using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashProjectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    private Vector2 movePosition;

    void Update()
    {
        MoveProjectile();
    }

    public void UpdateMovePostionVector(Vector2 targetPos) {
        movePosition = targetPos;
    }

    private void MoveProjectile() {
        transform.Translate(new Vector3(1, 1, 0) * Time.deltaTime * moveSpeed);
    }

    // anim event
    public void DestroySelf() {
        Destroy(gameObject);
    }
}
