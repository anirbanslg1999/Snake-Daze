using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : MonoBehaviour
{
    //Using enums to define the Character number to not make different scrpits for it
    [SerializeField] characterTypes character;
    private Vector2 direction;
    private Vector2 savedDirection;

    // having the body segments
    [SerializeField] Transform snakeBodySegment;
    //make a list for the body
    private List<Transform> segment;

    // Accessing the player controller script by name a object in the awake methord
    private PlayerController playerController;
    // To store the input action inside it.
    InputAction move;

    [SerializeField] float lerpTime;
    private void Awake()
    {
        // making the object of class player controller
        playerController = new PlayerController();
        //checking on which input type the playercontroller should be passed.
        if (character == characterTypes.player1)
        {
            move = playerController.Snake1.Move;
        }
        else if (character == characterTypes.player2)
        {
            move = playerController.Snake2.Move;
        }
        // subscribing to the event 
        move.Enable();
        move.performed += OnMove;    
    }
    private void OnDisable()
    {
        move.Disable();
    }
    private void Start()
    {
        segment = new List<Transform>();
        segment.Add(this.transform);
        AudioManager.Instance.PlayBackGroundSound(SoundTypes.GameBG);
        
    }
    void Update()
    {
        SetDirection();
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        // Storing the input. 
        direction = context.ReadValue<Vector2>();
    }
    private void SetDirection()
    {
        // Defining the direction to go and where not to go... like cant go in the opposide direction.
        if (direction.x != 0 && direction.y != 0)
        {
            direction = new Vector2(savedDirection.x, savedDirection.y);
        }
        else if ((direction.x > 0 && savedDirection.x < 0) || (direction.x < 0 && savedDirection.x > 0))
        {
            direction.x = savedDirection.x;
        }
        else if ((direction.y > 0 && savedDirection.y < 0) || (direction.y < 0 && savedDirection.y > 0))
        {
            direction.y = savedDirection.y;
        }
    }

    private void FixedUpdate()
    {
        for (int i = segment.Count - 1; i > 0; i--)
        {
            segment[i].position =  segment[i - 1].position;

        }
        // changing the transform of the player to move.
        this.transform.position = new Vector3(Mathf.Round(transform.position.x) + direction.x, Mathf.Round(transform.position.y) + direction.y, 0);
        // storig the value to compare it.
        savedDirection = direction;
    }

    private void GrowBody()
    {
        Transform body = Instantiate(snakeBodySegment);
        body.position = segment[segment.Count - 1].position;
        segment.Add(body);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<ColliderTag>(out ColliderTag tags))
        {
            if(tags.type == ColliderTag.ColliderTags.Food)
            {
                GrowBody();
            }
            else if (tags.type == ColliderTag.ColliderTags.Boundary)
            {
                UIManager.Instance.GameOverScenarioOneWithBoundary(character);
            }
            else if(tags.type == ColliderTag.ColliderTags.SnakeBody1)
            {
                if(character == characterTypes.player1)
                {
                    UIManager.Instance.GameOverScenarioThreePlayerEatsHisOwnBody(character);
                }
                else
                {
                    UIManager.Instance.GameOverScenarioTwoEating(character);
                }
            }
            else if(tags.type == ColliderTag.ColliderTags.SnakeBody2)
            {
                if (character == characterTypes.player2)
                {
                    UIManager.Instance.GameOverScenarioThreePlayerEatsHisOwnBody(character);
                }
                else
                {
                    UIManager.Instance.GameOverScenarioTwoEating(character);
                }
            }
        }
    }

    public enum characterTypes
    {
        player1,
        player2,
    }
}
