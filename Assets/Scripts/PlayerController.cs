using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    [SerializeField] private TrailRenderer myTrailRenderer;

    private Vector2 movement;
    public bool facingLeft = false;

    private bool isDashing = false;

    public static PlayerController instance;

    private void Awake() {
        instance = this;
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
        } else {
            myAnimator.SetFloat("moveX", 1f);
            facingLeft = false;
        }
    }
    
    private void Move() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        if (myAnimator.GetFloat("moveX") < -.1f) {
            mySpriteRenderer.flipX = true;
        } else {
            mySpriteRenderer.flipX = false;
        }
    }

    private void Dash() {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing) {
            moveSpeed = moveSpeed * dashSpeed;
            myTrailRenderer.emitting = true;
            isDashing = true;
            StartCoroutine(EndDashCo());
        }
    }

    private IEnumerator EndDashCo() {
        yield return new WaitForSeconds(.15f);
        moveSpeed = moveSpeed / dashSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(.2f);
        isDashing = false;
    }


}
