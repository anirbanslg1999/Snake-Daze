using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : MonoBehaviour
{
    private Vector2 direction;
    private Vector2 savedDirection;
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
        if (context.performed)
        {
        direction = context.ReadValue<Vector2>();
        }
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
}
