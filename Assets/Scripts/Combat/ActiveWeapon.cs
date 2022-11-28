using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D weaponTriggerCollider;
    [SerializeField] private Transform animSpawnPointPivot;
    [SerializeField] private MonoBehaviour activeWeapon = null;

    private bool isAttacking = true;
    private float timeBetweenAttacks;

    private void OnEnable() {
        isAttacking = true;
        DoneAttacking();
    }

    private void Update() {
        Attack();
    }

    public MonoBehaviour ReturnActiveWeapon() {
        return activeWeapon;
    }

    public Transform ReturnAnimSpawnPoint() {
        return animSpawnPointPivot;
    }

    public void NewWeapon() {
        foreach (Transform child in this.transform)
        {
            if (child.GetComponent<MonoBehaviour>() is IWeapon && child.gameObject.activeInHierarchy) {
                activeWeapon = child.GetComponent<MonoBehaviour>();
                break;
            }
        };

        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksCo());
        timeBetweenAttacks = (activeWeapon as IWeapon).ReturnWeaponInfo().weaponCooldown;
    }

    public void WeaponNull() {
        activeWeapon = null;
    }

    public void ToggleMouseFollow(bool boolValue) {
        GetComponent<MouseFollow>().enabled = boolValue;
    }

    private void Attack() {
        if (Input.GetMouseButton(0) && !isAttacking) {
            
            if (activeWeapon is IWeapon) {
                isAttacking = true;
                (activeWeapon as IWeapon).Attack();
            }
        }
    }

    // Animation Event
    public void DoneAttacking() {
        weaponTriggerCollider.enabled = false;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksCo());
    }

    private IEnumerator TimeBetweenAttacksCo() {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }


}
