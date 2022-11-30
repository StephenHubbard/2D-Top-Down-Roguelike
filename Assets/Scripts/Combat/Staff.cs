using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private float weaponRotOffset = 20f;
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform magicSpawnPoint;

    private Transform playerPos;
    private ActiveWeapon activeWeapon;
    private Animator myAnimator;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void Start() {
        playerPos = PlayerController.Instance.transform;
    }

    private void OnEnable() {
        activeWeapon.ToggleMouseFollow(false);
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    public WeaponInfo ReturnWeaponInfo() {
        return weaponInfo;
    }

    private void MouseFollowWithOffset() {
        Vector3 mousePos = Input.mousePosition;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(playerPos.position);
        var localY = transform.localScale.y;

        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        var angle =  Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        
        if(mousePos.x < playerScreenPoint.x) {
            activeWeapon.transform.eulerAngles = new Vector3 (0, -180, 0);
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle + weaponRotOffset);
        } else {
            activeWeapon.transform.eulerAngles = new Vector3 (0, 0, 0);
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle + weaponRotOffset);
        }
    }

    public void Attack()
    {
        myAnimator.SetTrigger("Attack");
    }

    public void SpawnStaffProjectile() {
        GameObject newLaser = Instantiate(magicLaser, magicSpawnPoint.position, activeWeapon.transform.rotation);
        MonoBehaviour activeWeaponInfo = activeWeapon.ReturnActiveWeapon();
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange((activeWeaponInfo as IWeapon).ReturnWeaponInfo().weaponRange);
        DoneAttack();
    }

    public void DoneAttack() {
        activeWeapon.DoneAttacking();
        myAnimator.SetTrigger("Done Attack");
    }
}
