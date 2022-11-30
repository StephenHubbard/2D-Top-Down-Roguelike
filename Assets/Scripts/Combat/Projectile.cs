using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameObject particleOnHitPrefab;

    private WeaponInfo weaponInfo;
    private Vector3 startPos;

    private void Start() {
        startPos = transform.position;
    }

    void Update()
    {
        MoveProjectile(); 
        DetectFireDistance();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Instantiate(particleOnHitPrefab, transform.position, transform.rotation);
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(weaponInfo.damageAmount);
            Destroy(gameObject);
        }
    }

    public void DetectFireDistance() {
        if (Vector3.Distance(transform.position, startPos) > weaponInfo.weaponRange) {
            Destroy(gameObject);
        }
    }

    public void UpdateWeaponInfo(WeaponInfo weaponInfo) {
        this.weaponInfo = weaponInfo;
    }

    public WeaponInfo ReturnWeaponInfo() {
        return weaponInfo;
    }

    private void MoveProjectile() {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    public void InstantiateParticleFX() {
        Instantiate(particleOnHitPrefab, transform.position, transform.rotation);
        AudioManager.Instance.Play("Slash Projectile Collide");
        Destroy(gameObject);
    }

    // anim event
    public void DestroySelf() {
        Destroy(gameObject);
    }
}
