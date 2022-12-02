using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShakeManager : Singleton<ScreenShakeManager>
{
    public static ScreenShakeManager instance { get; private set; }

    public CinemachineImpulseSource source;

    private void Start() {
        source = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeScreen() {
        source.GenerateImpulse();
    }
}
