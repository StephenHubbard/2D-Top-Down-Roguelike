using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float heightY = 3f;
    [SerializeField] private GameObject vfxOnHitPrefab;
    [SerializeField] private GameObject splatterPrefab;
    [SerializeField] private GameObject grapeProjectileShadow;


    private void Start() {
        GameObject shadow = Instantiate(grapeProjectileShadow, transform.position + new Vector3(0, -.3f, 0), transform.rotation);
        shadow.GetComponent<GrapeProjectileShadow>().SetFloatDuration(duration);
        StartCoroutine(CurveSpawnCo(transform.position, PlayerController.instance.GetPosition()));
        AudioManager.instance.Play("Grape Shoot");
    }

    private IEnumerator CurveSpawnCo(Vector3 start, Vector3 target) {
        float timePassed = 0f;

        Vector2 end = target;
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);
            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);
            yield return null;
        }

        Instantiate(vfxOnHitPrefab, transform.position, transform.rotation);
        Instantiate(splatterPrefab, transform.position, transform.rotation);
        AudioManager.instance.Play("Slime Death");
        Destroy(gameObject);
    }
}
