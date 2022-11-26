using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Parallax : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] Transform player;
    [SerializeField] private float parallexOffset = -0.15f;

    private Vector2 startPos;
    float startZ;

    private Vector2 travel => (Vector2)cam.transform.position - startPos;
    Vector2 parallexFactor;

    private void Start() {
        startPos = transform.position;
        startZ = transform.position.z;
    }

    private void FixedUpdate() {
        transform.position = startPos + travel * parallexOffset;
    }
}
