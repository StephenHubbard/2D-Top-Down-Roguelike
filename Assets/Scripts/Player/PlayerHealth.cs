using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    #region Public Variables

    public int currentHealth;
    public int maxHealth;

    #endregion

    #region Private Variables

    public static PlayerHealth instance { get; private set; }
    
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float whiteFlashTime = .1f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private float respawnTimeFloat = 2f;
    [SerializeField] private Slider healthSlider;
    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    private bool canTakeDamage = true;
    private bool isDead = false;
    private Rigidbody2D rb;

    #endregion

    #region Unity Methods

    private void Awake() {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = startingHealth;
        maxHealth = startingHealth;
        defaultMat = spriteRenderer.material;
    }

    private void Start() {
        healthSlider.maxValue = startingHealth;
    }

    private void OnCollisionStay2D(Collision2D other) { 
        if (other.gameObject.CompareTag("Enemy") && canTakeDamage && currentHealth > 0) {
            AudioManager.instance.Play("Hero Take Damage");
            EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
            TakeDamage(enemy.damageDoneToHero);
            GetComponent<KnockBack>().getKnockedBack(other.gameObject.transform, enemy.enemyKnockBackThrust);
            ScreenShakeManager.instance.ShakeScreen();
        }
    }

    #endregion

    #region Public Methods

    public void CheckIfDeath() {
        if (currentHealth <= 0 && !isDead) {
            // isDead set to prevent death animation from triggering multiple times
            isDead = true;
            // PlayerController.instance.canMove = false;
            // PlayerController.instance.canAttack = false;
            // myAnimator.SetTrigger("dead");
            Destroy(gameObject);
        } else {
            // PlayerController.instance.canMove = true;
            // PlayerController.instance.canAttack = true;
        }
    }

    public void TakeDamage(int damage) {
        spriteRenderer.material = whiteFlashMat;
        currentHealth -= damage;
        canTakeDamage = false;
        UpdateHealthSlider();
        StartCoroutine(SetDefaultMatRoutine());
        StartCoroutine(DamageRecoveryTimeRoutine());
        CheckIfDeath();
    }

    public void HealSelf(int amount) {
        currentHealth += amount;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
        UpdateHealthSlider();
    }

    private void UpdateHealthSlider() {
        healthSlider.value = currentHealth;
    }

    #endregion

    #region Private Coroutines

    private IEnumerator SetDefaultMatRoutine() {
        yield return new WaitForSeconds(whiteFlashTime);
        spriteRenderer.material = defaultMat;
    }

    private IEnumerator DamageRecoveryTimeRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    // private IEnumerator RespawnRoutine() {
    //     yield return new WaitForSeconds(respawnTimeFloat);
    //     Destroy(PlayerController.instance.gameObject);
    //     SceneManager.LoadScene("Town");
    // }

    #endregion
}
