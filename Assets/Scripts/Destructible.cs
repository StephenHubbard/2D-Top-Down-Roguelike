using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private Animator myAnimator;
    private Collider2D myCollider;
    [SerializeField] private GameObject destroyVFX;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            GetComponent<Booty>().DropItems();
            // myAnimator.SetTrigger("Destroy");
            Instantiate(destroyVFX, transform.position, transform.rotation);
            if (other.gameObject.GetComponent<SlashProjectile>()) {
                other.gameObject.GetComponent<SlashProjectile>().InstantiateParticleFX();
            }
            Destroy(gameObject);
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
