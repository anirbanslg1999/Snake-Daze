using UnityEngine;

public class ColliderTag : MonoBehaviour
{
    public ColliderTags type;
    public enum ColliderTags
    {
        Snake1,
        Snake2,
        SnakeBody1,
        Food,
        Creatures,
        Boundary,
        SnakeBody2
    }
}
