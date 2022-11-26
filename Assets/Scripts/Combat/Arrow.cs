using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float flySpeed = 10f;

    private void Update() {
        MoveProjectile();
    }

    private void MoveProjectile() {
        transform.Translate(Vector3.right * Time.deltaTime * flySpeed);
    }

    
}
