using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameObject particleOnHitPrefab;

    // private Vector2 movePosition;

    void Update()
    {
        MoveProjectile();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile() {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    public void InstantiateParticleFX() {
        Instantiate(particleOnHitPrefab, transform.position, transform.rotation);
        AudioManager.instance.Play("Slash Projectile Collide");
        Destroy(gameObject);
    }

    // anim event
    public void DestroySelf() {
        Destroy(gameObject);
    }
}
