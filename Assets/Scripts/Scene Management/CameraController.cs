using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake() {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Start()
    {
        if (FindObjectOfType<PlayerController>()) {
            player = PlayerController.Instance.transform;
        }
    }

    private void Update() {
        FindPlayer();
    }

    private void FindPlayer() {
        if (player == null) {
            player = PlayerController.Instance.transform;
        }

        cinemachineVirtualCamera.Follow = player;
    }
}
