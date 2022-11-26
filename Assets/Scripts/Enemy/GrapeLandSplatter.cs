using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeLandSplatter : MonoBehaviour
{
    [SerializeField] private float fadeTime = 1f;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        StartCoroutine(SlowFadeCo()); 
        StartCoroutine(DetectPlayerDamageOnLand());
    }

    private IEnumerator DetectPlayerDamageOnLand() {
        yield return new WaitForSeconds(.2f);
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    private IEnumerator SlowFadeCo()
    {
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

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            other.gameObject.GetComponent<KnockBack>().getKnockedBack(transform, 5f);
        }
    }

    
}
