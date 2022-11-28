using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] private float parallexOffset = -0.15f;

    private Vector2 startPos;

    private Vector2 travel => (Vector2)cam.transform.position - startPos;

    private void Awake() {
        cam = Camera.main;
    }

    private void Start() {
        startPos = transform.position;
    }

    private void FixedUpdate() {
        transform.position = startPos + travel * parallexOffset;
    }

   
}
