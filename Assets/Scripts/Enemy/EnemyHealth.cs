using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public enum EnemyType {
        None, 
        Grape, 
        Slime,
    }

    [SerializeField] private EnemyType enemyType;
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Material matWhiteFlash;
    [SerializeField] private float setDefaultMatRestorefloat = .1f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private string enemyHitStringSFX = "";
    [SerializeField] private string enemyDeathStringSFX = "";

    private bool canTakeDamage = true;
    private Material matDefault;
    private SpriteRenderer spriteRenderer;
    private KnockBack knockBack;


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

    public void TakeDamage(int damage) {
        AudioManager.Instance.Play(enemyHitStringSFX);
        canTakeDamage = false;
        knockBack.getKnockedBack(PlayerController.Instance.transform, 15f);
        StartCoroutine(DamageRecoveryTimeRoutine());
        currentHealth -= damage;
        spriteRenderer.material = matWhiteFlash;
        StartCoroutine(SetDefaultMatRoutine(setDefaultMatRestorefloat));
    }

    private void DetectDeath() {
        if (currentHealth <= 0) {
            GameObject deathVFX = Instantiate(deathVFXPrefab, transform.position, transform.rotation);
            GetComponent<Booty>().DropItems();
            AudioManager.Instance.Play(enemyDeathStringSFX);
            QuestUpdate();
            Destroy(gameObject);
        }
    }

    private void QuestUpdate() {
        foreach (var task in TaskManager.Instance.ReturnAllActiveTasks())
        {
            if (task.taskGoal.goalType == TaskGoal.GoalType.Kill && task.isActive) {
                task.taskGoal.EnemyKilled(enemyType);
            }

            if (task.taskGoal.IsReached() && task.isActive) {
                task.TaskComplete();
            }
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
