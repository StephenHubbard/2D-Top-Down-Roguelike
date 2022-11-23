using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region Private Variables 

    [SerializeField] private int startingHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Material matWhiteFlash;
    [SerializeField] private float setDefaultMatRestorefloat = .1f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private GameObject deathVFXPrefab;

    private bool canTakeDamage = true;
    private Material matDefault;
    private SpriteRenderer spriteRenderer;
    private KnockBack knockBack;

    #endregion

    private void Awake() {
        knockBack = GetComponent<KnockBack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        matDefault = spriteRenderer.material;
    }

    private void Start() {
        currentHealth = startingHealth;
    }

    private void Update() { 
        DetectDeath();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && canTakeDamage) {
            TakeDamage(1);
            canTakeDamage = false;
            knockBack.getKnockedBack(PlayerController.instance.transform, 15f);
            StartCoroutine(DamageRecoveryTimeRoutine());
        }
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        spriteRenderer.material = matWhiteFlash;
        StartCoroutine(SetDefaultMatRoutine(setDefaultMatRestorefloat));
    }

    private void DetectDeath() {
        if (currentHealth <= 0) {
            Instantiate(deathVFXPrefab, transform.position, transform.rotation);
            GetComponent<Booty>().DropItems();
            Destroy(gameObject);
        }
    }

    

    private IEnumerator SetDefaultMatRoutine(float whiteFlashTime) {
        yield return new WaitForSeconds(whiteFlashTime);
        spriteRenderer.material = matDefault;
    }

    private IEnumerator DamageRecoveryTimeRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

}
