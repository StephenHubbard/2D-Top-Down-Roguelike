using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public AreaEntrance theEntrance;
    public float waitToLoad = 1f; 

    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    private bool shouldLoadAfterFade; 

    private void Start() {
        theEntrance.transitionName = sceneTransitionName;
    }

    private void Update() { 
        if(shouldLoadAfterFade) {
            waitToLoad -= Time.deltaTime;
            if(waitToLoad <= 0) {
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            shouldLoadAfterFade = true;
            UIFade.Instance.FadeToBlack();

            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
        }
    }
}
