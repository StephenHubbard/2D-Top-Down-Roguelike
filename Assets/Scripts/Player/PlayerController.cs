using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public bool facingLeft = false;
    public bool canMove = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float dashTime = .2f;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Transform weaponHitCollider;

    private Vector2 movement;
    private bool isDashing = false;
    

    private KnockBack knockBack;
    private Stamina stamina;
    private PlayerHealth playerHealth;

    protected override void Awake() {
        base.Awake();

        knockBack = GetComponent<KnockBack>();
        stamina = GetComponent<Stamina>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update() {
        PlayerInput();
        Dash();
    }

    private void FixedUpdate() {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void LateUpdate() {
    }


    private void PlayerInput() {
        if (!canMove) { return; }

        if (Input.GetKey(KeyCode.W)) movement.y = +1f;
        if (Input.GetKey(KeyCode.S)) movement.y = -1f;
        if (Input.GetKey(KeyCode.A)) movement.x = -1f;
        if (Input.GetKey(KeyCode.D)) movement.x = +1f;

        if (Input.GetKeyUp(KeyCode.W)) movement.y = 0f;
        if (Input.GetKeyUp(KeyCode.S)) movement.y = 0f;
        if (Input.GetKeyUp(KeyCode.A)) movement.x = 0f;
        if (Input.GetKeyUp(KeyCode.D)) movement.x = 0f;

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);

    }

    private void AdjustPlayerFacingDirection() {
        Vector3 mousePos = Input.mousePosition;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if(mousePos.x < playerScreenPoint.x) {
            myAnimator.SetFloat("moveX", -1f);
            facingLeft = true;
            weaponHitCollider.rotation = Quaternion.Euler(0, 180, 0);
        } else {
            myAnimator.SetFloat("moveX", 1f);
            facingLeft = false;
            weaponHitCollider.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    
    private void Move() {
        if (knockBack.ReturnGettingKnockedBack() || playerHealth.isDead) { return; }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        if (myAnimator.GetFloat("moveX") < -.1f) {
            mySpriteRenderer.flipX = true;
        } else {
            mySpriteRenderer.flipX = false;
        }
    }

    private void Dash() {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && stamina.currentStamina > 0 && !playerHealth.isDead) {
            stamina.UseStamina();
            moveSpeed = moveSpeed * dashSpeed;
            myTrailRenderer.emitting = true;
            isDashing = true;
            StartCoroutine(EndDashCo());
        }
    }

    private IEnumerator EndDashCo() {
        AudioManager.Instance.Play("Dash");
        yield return new WaitForSeconds(dashTime);
        moveSpeed = moveSpeed / dashSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(.2f);
        isDashing = false;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }


}
