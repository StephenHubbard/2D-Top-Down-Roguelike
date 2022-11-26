using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Knockback class can be put on gameobjects that you want to thrust back with rigidbody force against other objects that would typically deal damage
public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockbackTime = .2f;
    private Rigidbody2D rb;

    private bool gettingKnockedBack = false;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public bool ReturnGettingKnockedBack() {
        return gettingKnockedBack;
    }


    public void getKnockedBack(Transform damageSource, float knockBackThrust) {
        Vector2 difference = transform.position - damageSource.position;
        difference = difference.normalized * knockBackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        gettingKnockedBack = true;
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine() {
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        gettingKnockedBack = false;

        // if KnockBack class is on our player game object
        if (GetComponent<PlayerController>()) {
            // GetComponent<PlayerHealth>().CheckIfDeath();
        }
    }


}
