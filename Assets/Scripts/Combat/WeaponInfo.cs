using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create New Weapon")]
public class WeaponInfo : ScriptableObject
{
    public int damageAmount;
    public float weaponRange;
    public GameObject weaponPrefab;
    public Sprite inventorySlotSprite;
}
