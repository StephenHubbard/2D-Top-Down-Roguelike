using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undestructable : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<SlashProjectile>()) {
            other.gameObject.GetComponent<SlashProjectile>().InstantiateParticleFX();
        }
    }
}
