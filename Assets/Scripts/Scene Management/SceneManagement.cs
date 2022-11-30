using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : Singleton<SceneManagement>
{
    private string sceneTransitionName;

    public string ReturnSceneTransitionName() {
        return sceneTransitionName;
    }

    public void SetTransitionName(string sceneTransitionName) {
        this.sceneTransitionName = sceneTransitionName;
    }

}
