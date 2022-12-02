using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject grapeProjectilePrefab;
    [SerializeField] private GameObject grapeSuckInVFX;

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
        grapeSuckInVFX.SetActive(true);
        if (transform.position.x - PlayerController.Instance.GetPosition().x < 0) {
            spriteRenderer.flipX = false;
            grapeSuckInVFX.transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        } else {
            spriteRenderer.flipX = true;
            grapeSuckInVFX.transform.parent.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    // anim event
    public void SpawnProjectile()
    {
        grapeSuckInVFX.SetActive(false);
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
