using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShakeManager : MonoBehaviour
{
    public static ScreenShakeManager instance { get; private set; }

    private CinemachineImpulseSource source;

    private void Awake() {
        instance = this;

        source = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeScreen() {
        source.GenerateImpulse();
    }
}
