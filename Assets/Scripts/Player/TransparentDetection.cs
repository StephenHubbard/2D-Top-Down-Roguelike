using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float transparencyAmount = .8f;

    [SerializeField] private Sprite[] spritesToFade;

    private Tilemap tilemap;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        if (GetComponent<Tilemap>()) {
            tilemap = GetComponent<Tilemap>();
        }
        if (GetComponent<SpriteRenderer>()) {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            if (tilemap) {
                StartCoroutine(FadeOutTileMap());
            } else if (spriteRenderer) {
                StartCoroutine(FadeOutSpriteRenderer());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            if (tilemap) {
                StartCoroutine(FadeInTileMap());
            } else if (spriteRenderer) {
                StartCoroutine(FadeInSpriteRenderer());
            }
        }
    }

    private IEnumerator FadeOutTileMap()
    {
        float fadeTime = .4f;
        float elapsedTime = 0;
        float startValue = tilemap.color.a;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, transparencyAmount, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeInTileMap()
    {
        float fadeTime = .4f;
        float elapsedTime = 0;
        float startValue = tilemap.color.a;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 1f, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeOutSpriteRenderer()
    {
        float fadeTime = .4f;
        float elapsedTime = 0;
        float startValue = spriteRenderer.color.a;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, transparencyAmount, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeInSpriteRenderer()
    {
        float fadeTime = .8f;
        float elapsedTime = 0;
        float startValue = spriteRenderer.color.a;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 1f, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }
}
