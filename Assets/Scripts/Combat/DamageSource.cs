using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private MonoBehaviour activeWeapon;

    private int damageAmount;

    private void Awake() {
        activeWeapon = FindObjectOfType<ActiveWeapon>().ReturnActiveWeapon();
    }

    private void Start() {
        this.damageAmount = (activeWeapon as IWeapon).ReturnWeaponInfo().damageAmount;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<EnemyHealth>()) {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
        }
    }
}
