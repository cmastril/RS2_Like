using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    //Events
    public UnityEvent startedWalkingLeft = new UnityEvent();
    public UnityEvent startedWalkingRight = new UnityEvent();
    public UnityEvent startedWalkingUp = new UnityEvent();
    public UnityEvent startedWalkingDown = new UnityEvent();

    //References
    [SerializeField] private Rigidbody2D rb;

    //Fields
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 movement;
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        TurnMovementToFourDirectional();
        MoveCharacter();
    }

    private void TurnMovementToFourDirectional()
    {
        Vector2 tempMove = movement;

        float x = Mathf.Abs(movement.x);
        float y = Mathf.Abs(movement.y);

        if (x > y)
        {
            tempMove.y = 0;
        }
        else
        {
            tempMove.x = 0;
        }

        movement = tempMove;
    }

    private void MoveCharacter()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (movement.x > 0)
        {
            startedWalkingRight.Invoke();
        }
        else if (movement.x < 0)
        {
            startedWalkingLeft.Invoke();
        }
        else if (movement.y < 0)
        {
            startedWalkingDown.Invoke();
        }
        else if (movement.y > 0)
        {
            startedWalkingUp.Invoke();
        }

    }

}
