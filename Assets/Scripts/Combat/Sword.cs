using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private GameObject slashProjectilePrefab;
    [SerializeField] private float weaponRotOffset = 20f;
    [SerializeField] private WeaponInfo weaponInfo;

    private PolygonCollider2D weaponTriggerCollider;
    private Transform slashProjectileSpawnPoint;
    private Transform playerPos;
    private GameObject slashAnim;
    private ActiveWeapon activeWeapon;
    private Animator myAnimator;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void Start() {
        playerPos = PlayerController.instance.transform;
        weaponTriggerCollider = GameObject.Find("WeaponTriggerCollider").GetComponent<PolygonCollider2D>();
        slashProjectileSpawnPoint = GameObject.Find("SlashProjectileSpawnPoint").transform;
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
        slashAnim = Instantiate(slashAnimPrefab, PlayerController.instance.transform.position, transform.rotation);
        slashAnim.transform.SetParent(PlayerController.instance.transform);
        weaponTriggerCollider.enabled = true;
        GameObject slashPrefab = Instantiate(slashProjectilePrefab, slashProjectileSpawnPoint.position, activeWeapon.ReturnAnimSpawnPoint().rotation);
        slashPrefab.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
        AudioManager.instance.Play("Sword Slash");
    }

    public void SwingUpFlipAnim() {
        if (slashAnim == null) { return; }
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.instance.facingLeft) {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnim() {
        if (slashAnim == null) { return; }
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.instance.facingLeft) {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void DoneAttack() {
        activeWeapon.DoneAttacking();
    }
}
