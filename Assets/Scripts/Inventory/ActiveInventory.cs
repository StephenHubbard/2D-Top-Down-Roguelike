using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    [SerializeField] private Transform activeInventoryContainer;

    private int activeSlotIndexNum = 0;

    private WeaponInfo itemToEquip;

    private void Start() {
        ChangeActiveWeapon();
    }

    private void Update() {
        ToggleActiveSlot();
    }

    private void ToggleActiveSlot() {
        if (Input.GetKeyDown(KeyCode.Alpha1) || 
            Input.GetKeyDown(KeyCode.Alpha2) || 
            Input.GetKeyDown(KeyCode.Alpha3) || 
            Input.GetKeyDown(KeyCode.Alpha4) || 
            Input.GetKeyDown(KeyCode.Alpha5)) 
        {
            for(int i=0;i<10;i++)
            {
                if(Input.GetKeyDown((KeyCode)(48+i)))
                {
                    ToggleActiveHighlight(i - 1);
                }
            }
        }
    }

    private void ToggleActiveHighlight(int indexNum) {
        activeSlotIndexNum = indexNum;

        foreach (Transform activeSlot in activeInventoryContainer)
        {
            activeSlot.GetChild(0).gameObject.SetActive(false);
        }

        activeInventoryContainer.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        if (activeSlotIndexNum <= 1) {
            ChangeActiveWeapon();
        } else {
            foreach (Transform availableWeapons in PlayerController.instance.transform.GetComponentInChildren<ActiveWeapon>().transform)
            {
                availableWeapons.gameObject.SetActive(false);
            }

            PlayerController.instance.transform.GetComponentInChildren<ActiveWeapon>().WeaponNull();
        }
    }

    private void ChangeActiveWeapon() {
        foreach (Transform availableWeapons in PlayerController.instance.transform.GetComponentInChildren<ActiveWeapon>().transform)
        {
            availableWeapons.gameObject.SetActive(false);
        }

        PlayerController.instance.transform.GetComponentInChildren<ActiveWeapon>().transform.GetChild(activeSlotIndexNum).gameObject.SetActive(true);
        
        PlayerController.instance.transform.GetComponentInChildren<ActiveWeapon>().NewWeapon();
        
    }
}
