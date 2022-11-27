using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject grapeProjectilePrefab;

    private Animator myAnimator;
    private EnemyAI enemyAI;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        enemyAI = GetComponent<EnemyAI>();
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack() {
        myAnimator.SetTrigger("Attack");
        if (transform.position.x - PlayerController.instance.GetPosition().x < 0) {
            spriteRenderer.flipX = false;
        } else {
            spriteRenderer.flipX = true;
        }
    }

    // anim event
    public void SpawnProjectile()
    {
        Vector3 projectileSpawnPos;

        if (GetComponent<SpriteRenderer>().flipX == false) {
            projectileSpawnPos = transform.position + new Vector3 (.7f, .5f);
        } else {
            projectileSpawnPos = transform.position + new Vector3 (-.7f, .5f);
        }

        Instantiate(grapeProjectilePrefab, projectileSpawnPos, transform.rotation);
        GetComponent<EnemyPathfindingMovement>().AllowedToMoveToggle(true);
        enemyAI.FindTarget();
    }

    

    
}
