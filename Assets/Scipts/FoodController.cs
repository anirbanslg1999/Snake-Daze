using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FoodController : MonoBehaviour
{
    [SerializeField] BoxCollider2D confiner;
    [SerializeField] List<Sprite> foodSprites = new List<Sprite>();
    private SpriteRenderer foodSpriteRenderer;

    private int randNumForSpriteSelection;

    // Taking the value from the inspector to add value to the food
    [SerializeField] int healthyFoodValue;
    [SerializeField] int specialHealthFoodValue;

    private int Score = 0;
    
    FoodType foodType;
    private void Awake()
    {
        foodSpriteRenderer = GetComponent<SpriteRenderer>();
        FoodPositionAllocation();
    }

    private FoodType DeterminingTheFoodType()
    {
        randNumForSpriteSelection = Random.Range(0, 100);
        if(randNumForSpriteSelection < 65)
        {
            foodType = FoodType.Healthy;
        }
        else
        {
            foodType = FoodType.SpecialFood;
        }
        return foodType;
    }
    private void FoodPositionAllocation()
    {
        foodType = DeterminingTheFoodType();
        if (foodType == FoodType.Healthy)
        {
            foodSpriteRenderer.sprite = foodSprites[0];  
        }
        else if(foodType == FoodType.SpecialFood)
        {
            foodSpriteRenderer.sprite = foodSprites[1];
        }
        Bounds bounds = confiner.bounds;
        float tempX = Random.Range(bounds.min.x, bounds.max.x);
        float tempY = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(Mathf.Round(tempX), Mathf.Round(tempY), 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<ColliderTag>(out ColliderTag tags))
        {
            if(tags.type == ColliderTag.ColliderTags.Snake1)
            {
                if (foodType == FoodType.Healthy)
                {
                    UIManager.Instance.IncrementCoinUI(healthyFoodValue);
                }
                else if (foodType == FoodType.SpecialFood)
                {
                    UIManager.Instance.IncrementCoinUI(specialHealthFoodValue);
                }
                AudioManager.Instance.PlayEffectSound(SoundTypes.Collectable);
                FoodPositionAllocation();
            }
            else if (tags.type == ColliderTag.ColliderTags.Snake2)
            {
                if (foodType == FoodType.Healthy)
                {
                    MultiplayerManager.Instance.IncrementCoinUI(healthyFoodValue);
                }
                else if (foodType == FoodType.SpecialFood)
                {
                    MultiplayerManager.Instance.IncrementCoinUI(specialHealthFoodValue);
                }
                AudioManager.Instance.PlayEffectSound(SoundTypes.Collectable);
                FoodPositionAllocation();
            }


        }
    }





}
public enum FoodType
{
    Healthy,
    SpecialFood
}
