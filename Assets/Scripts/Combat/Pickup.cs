using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType {
        None,
        HealthGlobe, 
        StaminaGlobe,
        GoldCoin,
    }

    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private PickupType pickupType;
    [SerializeField] private int healGlobeAmount = 1; 
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float heightY = 3f;

    private Vector3 moveDir;
    private Rigidbody2D myRb;

    private void Awake() {
        myRb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        StartCoroutine(CurveSpawnCo(transform.position, new Vector2(transform.position.x + Random.Range(-2f, 2f), transform.position.y)));
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.Instance.GetPosition()) < pickUpDistance) {
            moveSpeed += .1f;
            moveDir = (PlayerController.Instance.GetPosition() - transform.position).normalized;
        } else {
            moveDir = Vector3.zero;
        }
    }


    private void FixedUpdate() {

        myRb.velocity = moveDir * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            DetectPickUpType();
            QuestUpdate();
            Destroy(gameObject);
        }
    }

    private void QuestUpdate() {
        foreach (var task in TaskManager.Instance.ReturnAllActiveTasks())
        {
            if (task.taskGoal.goalType == TaskGoal.GoalType.Gather && task.isActive) {
                task.taskGoal.ItemGathered(pickupType);
            }

            if (task.taskGoal.IsReached() && task.isActive) {
                task.TaskComplete();
            }
        }
    }

    private IEnumerator CurveSpawnCo(Vector3 start, Vector2 target) {
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
    }

    private void DetectPickUpType() {
        switch (pickupType)
        {
            default:
            case PickupType.HealthGlobe:
                PlayerHealth.Instance.HealSelf(healGlobeAmount);
                AudioManager.Instance.Play("Health Globe");
            break;

            case PickupType.StaminaGlobe:
                Stamina.instance.RefreshStamina();
                AudioManager.Instance.Play("Stamina Globe");
            break;

            case PickupType.GoldCoin:
                EconomyManager.Instance.ChangeCurrentGold(1);
                AudioManager.Instance.Play("Coin");
            break;
        }
    }

    
}
