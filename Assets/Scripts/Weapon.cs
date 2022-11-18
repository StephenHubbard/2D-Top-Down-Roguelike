using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private float weaponRotOffset = 20f;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private float slashAnimRotOffset = 130f;
    [SerializeField] private float timeBetweenAttacks = 1f;
    [SerializeField] private PolygonCollider2D weaponTriggerCollider;
    [SerializeField] private GameObject slashProjectilePrefab;
    [SerializeField] private Transform slashProjectileSpawnPoint;
    [SerializeField] private Transform animSpawnPointPivot;

    private bool isAttacking = true;
    private GameObject slashAnim;

    private void Start() {
        DoneAttacking();
    }

    private void Update() {
        MouseFollowWithOffset();
        Attack();
    }

    private void MouseFollowWithOffset() {
        Vector3 mousePos = Input.mousePosition;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(playerPos.position);
        var localY = transform.localScale.y;

        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        var angle =  Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        
        if(mousePos.x < playerScreenPoint.x) {
            transform.eulerAngles = new Vector3 (0, -180, 0);
            transform.rotation = Quaternion.Euler(0, -180, angle + weaponRotOffset);
        } else {
            transform.eulerAngles = new Vector3 (0, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, angle + weaponRotOffset);
        }
    }

    private void Attack() {
        if (Input.GetMouseButton(0) && !isAttacking) {
            // Time.timeScale = .2f;
            isAttacking = true;
            myAnimator.SetTrigger("attack");
            slashAnim = Instantiate(slashAnimPrefab, PlayerController.instance.transform.position, transform.rotation);
            slashAnim.transform.SetParent(PlayerController.instance.transform);
            weaponTriggerCollider.enabled = true;
            Instantiate(slashProjectilePrefab, slashProjectileSpawnPoint.position, animSpawnPointPivot.rotation * Quaternion.Euler(0, 0, -45f));
        }
    }

    public void SwingUpFlipAnim() {
        if (slashAnim == null) { return; }
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.instance.facingLeft) {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim() {
        if (slashAnim == null) { return; }
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.instance.facingLeft) {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Animation Event
    public void DoneAttacking() {
        weaponTriggerCollider.enabled = false;
        StartCoroutine(TimeBetweenAttacksCo());
    }

    private IEnumerator TimeBetweenAttacksCo() {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

}
