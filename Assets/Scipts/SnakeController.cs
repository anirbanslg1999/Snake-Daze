using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : MonoBehaviour
{
    [SerializeField] characterTypes character;
    private Vector2 direction;
    private Vector2 savedDirection;

    private PlayerController playerController;
    InputAction move;
    private void Awake()
    {
        playerController = new PlayerController();
        if(character == characterTypes.player1)
        {
        move = playerController.Snake1.Move;
        }
        else if (character == characterTypes.player2)
        {
        move = playerController.Snake2.Move;
        }
        move.performed += OnMove;
        move.Enable();
    }
    private void Start()
    {
        savedDirection = direction;

    }
    void Update()
    {
        SetDirection();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }
    private void SetDirection()
    {
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
        this.transform.position = new Vector3(Mathf.Round(transform.position.x) + direction.x, Mathf.Round(transform.position.y) + direction.y, 0);
        savedDirection = direction;
    }

    private enum characterTypes
    {
        player1,
        player2,
    }
}
