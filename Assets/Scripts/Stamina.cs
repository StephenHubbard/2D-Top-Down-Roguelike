using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public int currentStamina;

    [SerializeField] private Sprite fullStamImage, emptyStamImage;
    [SerializeField] private float timeBetweenStaminaRefresh;
    [SerializeField] private Transform staminaContainer;
    
    private void Awake() {
        currentStamina = 3;
    }

    private void Start() {

    }

    private IEnumerator RefreshStaminaCD() {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
            RefreshStamina();
        }
    }

    public void UseStamina() {
        currentStamina--;
        UpdateStaminaImages();
    }

    public void RefreshStamina() {
        if (currentStamina < 3) {
            currentStamina++;
            UpdateStaminaImages();
        }
    }

    private void UpdateStaminaImages() {
        for (int i = 0; i < 3; i++)
        {
            if (i <= currentStamina - 1) {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = fullStamImage;
            } else {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = emptyStamImage;
            }
        }

        if (currentStamina < 3) {
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaCD());
        }
    }
}
