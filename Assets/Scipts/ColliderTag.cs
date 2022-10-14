using UnityEngine;

public class ColliderTag : MonoBehaviour
{
    public ColliderTags type;
    public enum ColliderTags
    {
        Snake,
        SnakeBody,
        Food,
        Creatures,
        Boundary
    }
}
