using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeProjectileShadow : MonoBehaviour
{
    private float duration;

    public void SetFloatDuration(float duration) {
        this.duration = duration;
        StartCoroutine(MoveTowardsProjectileTargetCo(transform.position, PlayerController.Instance.GetPosition()));
    }

    private IEnumerator MoveTowardsProjectileTargetCo(Vector3 start, Vector3 target) {
        float timePassed = 0f;

        Vector2 end = target;
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            transform.position = Vector2.Lerp(start, end, timePassed / duration);
            yield return null;
        }

        Destroy(gameObject);
    }
}
