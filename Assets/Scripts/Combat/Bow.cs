using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private WeaponInfo weaponInfo;

    private ActiveWeapon activeWeapon;
    private Animator myAnimator;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void OnEnable() {
        activeWeapon.ToggleMouseFollow(true);
    }

    public WeaponInfo ReturnWeaponInfo()
    {
        return weaponInfo;
    }

    public void Attack()
    {
        myAnimator.SetTrigger("Fire");
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, activeWeapon.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
        AudioManager.instance.Play("Bow Fire");
    }

    public void DoneAttack() {
        activeWeapon.DoneAttacking();
    }

    
}
