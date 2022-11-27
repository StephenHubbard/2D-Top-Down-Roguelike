using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime = .7f;
    [SerializeField] private GameObject projectileParticleFX;
    [SerializeField] private LayerMask undestructibleLayerMask = new LayerMask();
    private Vector3 mousePos;
    private CapsuleCollider2D capsuleCollider;
    private float laserRange;
    private SpriteRenderer spriteRenderer;

    private bool isGrowing = true;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start() {
        LaserFaceMouse();

        AudioManager.instance.Play("Laser Fire");
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Indestructible>()) {
            isGrowing = false;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, undestructibleLayerMask);

            Instantiate(projectileParticleFX, hit.point, transform.rotation);
        }
    }

    public void UpdateLaserRange(float laserRange) {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserLengthCo());
        capsuleCollider.size = new Vector2(laserRange, capsuleCollider.size.y);
    }

    private void LaserFaceMouse() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(transform.position.x - mousePosition.x, transform.position.y - mousePosition.y);

        transform.right = -direction;
    }

    private IEnumerator IncreaseLaserLengthCo() {
        float timePassed = 0f;

        while (spriteRenderer.size.x < laserRange && isGrowing)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / laserGrowTime;

            // collider
            capsuleCollider.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), capsuleCollider.size.y);
            capsuleCollider.offset = new Vector2((Mathf.Lerp(1f, laserRange, linearT)) / 2, capsuleCollider.offset.y);

            // sprite
            spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), 1);
           

            yield return null;    
        }

        StartCoroutine(SlowFadeCo());
    }

    private IEnumerator SlowFadeCo()
    {
        float fadeTime = .5f;
        float elapsedTime = 0;
        float startValue = spriteRenderer.color.a;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 0f, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    
}
