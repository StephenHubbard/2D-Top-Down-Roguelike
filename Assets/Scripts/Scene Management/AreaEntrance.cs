using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour 
{
    [SerializeField] private string transitionName;
    [SerializeField] private float moveSpeedWaitTime = .5f;
    [SerializeField] private string musicString;

    private void Start() {  
        if (PlayerController.Instance != null) {
            if (transitionName == SceneManagement.Instance.ReturnSceneTransitionName()) {
                PlayerController.Instance.transform.position = transform.position;
                StartCoroutine(HeroMoveDelayRoutine());

                if (UIFade.Instance != null) {
                    UIFade.Instance.FadeToClear();
                }
            }
        }
        AudioManager.Instance.PlaySceneMusic();
    }

    public string ReturnMusicString() {
        return musicString;
    }

    public string ReturnTransitionName() {
        return transitionName;
    }

    private IEnumerator HeroMoveDelayRoutine() {
        PlayerController.Instance.canMove = false;
        yield return new WaitForSeconds(moveSpeedWaitTime);
        PlayerController.Instance.canMove = true;
    }
}
