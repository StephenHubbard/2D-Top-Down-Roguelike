using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
    [SerializeField] private TMP_Text goldText;

    private int currentGold = 0;

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
