using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusObjectController : MonoBehaviour
{
    [SerializeField] BoxCollider2D confiner;
    [SerializeField] Sprite bonusSprite;
    [SerializeField] int bonusCoinAmount;
    private SpriteRenderer bonusSpriteRenderer;

    private bool willSpawn =false;
    [SerializeField] float spawnAfterTime;
    float timeSpent = 0;
    private float timeLimit;
    private bool hasBonusObjectSpawned = false;
    [SerializeField] int bonusObjectPoint;

    [SerializeField] Transform holdPosition;

    private void Awake()
    {
        bonusSpriteRenderer = GetComponent<SpriteRenderer>();
        timeLimit = spawnAfterTime;
    }
    private void Update()
    {
        if(spawnAfterTime > 0)
        {
            spawnAfterTime -= Time.deltaTime;
        }
        else
        {
            spawnAfterTime = timeLimit;
            FoodPositionAllocation();
        }
        if (hasBonusObjectSpawned)
        {
            if(timeSpent < 10)
            {
                timeSpent += Time.deltaTime;
            }
            else
            {
                timeSpent = 0;
                hasBonusObjectSpawned=false;
                this.transform.position = holdPosition.position;
            }
        }
    }

    private bool spawnProbability()
    {
        int temp = Random.Range(0, 100);
        if(temp > 70)
        {
            willSpawn = true;
        }
        else willSpawn = false;
        return willSpawn;
    }
    private void FoodPositionAllocation()
    {
        if (spawnProbability())
        {
            hasBonusObjectSpawned = true;
            UIManager.Instance.StartDisplayIcon();
            bonusSpriteRenderer.sprite = bonusSprite;
            Bounds bounds = confiner.bounds;
            float tempX = Random.Range(bounds.min.x, bounds.max.x);
            float tempY = Random.Range(bounds.min.y, bounds.max.y);
            this.transform.position = new Vector3(Mathf.Round(tempX), Mathf.Round(tempY), 0);
        }
        else return;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<ColliderTag>(out ColliderTag tags))
        {
            if(tags.type == ColliderTag.ColliderTags.Snake1)
            {
                this.transform.position = holdPosition.position;
                UIManager.Instance.StopDisplayingIcon();
                UIManager.Instance.IncrementCoinUI(bonusCoinAmount);
            }
        }
    }
}
