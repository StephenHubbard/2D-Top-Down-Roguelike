using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance { get; private set; }

    private TMP_Text goldText;

    private int currentGold = 0;

    private void Awake() {
        instance = this;
    }

    private void Update() {
        if (goldText == null) {
            goldText = GameObject.Find("Coin Amount Text").GetComponent<TMP_Text>();
        }

        goldText.text = currentGold.ToString();
    }

    public void ChangeCurrentGold(int amount) {
        currentGold += amount;
    }
}
