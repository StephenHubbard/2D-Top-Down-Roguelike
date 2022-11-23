using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booty : MonoBehaviour
{
    [SerializeField] private GameObject healthGlobe, staminaGlobe, goldCoin;
    [SerializeField] private bool isEnemy = false;
    

    public void DropItems() {
        int randomNum = Random.Range(1, 5);

        if (randomNum == 1) {
            Instantiate(healthGlobe, transform.position, transform.rotation);
        } else if (randomNum == 2 && isEnemy) {
            Instantiate(staminaGlobe, transform.position, transform.rotation);
        }
    }
}
