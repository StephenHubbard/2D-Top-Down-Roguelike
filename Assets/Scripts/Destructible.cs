using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private Animator myAnimator;
    private Collider2D myCollider;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            myAnimator.SetTrigger("Destroy");
            other.gameObject.GetComponent<SlashProjectile>().InstantiateParticleFX();
        }
    }

    // anim event
    public void DestroyAnimEvent() {
        StartCoroutine(DestroySelfCo());
        myCollider.enabled = false;
    }

    public IEnumerator DestroySelfCo() {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
