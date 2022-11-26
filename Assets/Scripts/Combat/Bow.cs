using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    private ActiveWeapon activeWeapon;
    private Animator myAnimator;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void OnEnable() {
        activeWeapon.ToggleMouseFollow(true);
    }

    public void Attack()
    {
        myAnimator.SetTrigger("Fire");
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, activeWeapon.transform.rotation);
        AudioManager.instance.Play("Bow Fire");
    }

    public void DoneAttack() {
        activeWeapon.DoneAttacking();
    }
}
