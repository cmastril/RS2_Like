using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimation : MonoBehaviour
{
    //References
    [SerializeField] private Animator animator;
    [SerializeField] private Movement movement;

    private void Start()
    {
        //Add Listeners
        movement.startedWalkingLeft.AddListener(WalkLeftHandle);
        movement.startedWalkingRight.AddListener(WalkRightHandle);
        movement.startedWalkingUp.AddListener(WalkUpHandle);
        movement.startedWalkingDown.AddListener(WalkDownHandle);
    }

    private void Update()
    {
        IdleHandle();
    }

    private void WalkLeftHandle()
    {
        PlayAnimation("Walk_Left");
    }

    private void WalkRightHandle()
    {
        PlayAnimation("Walk_Right");
    }

    private void WalkUpHandle()
    {
        PlayAnimation("Walk_Up");
    }

    private void WalkDownHandle()
    {
        PlayAnimation("Walk_Down");
    }

    private void IdleHandle()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (currentAnimation == "Walk_Right" && movement == new Vector2(0, 0))
        {
            PlayAnimation("Idle_Right");
        }
        else if (currentAnimation == "Walk_Left" && movement == new Vector2(0, 0))
        {
            PlayAnimation("Idle_Left");
        }
        else if (currentAnimation == "Walk_Up" && movement == new Vector2(0, 0))
        {
            PlayAnimation("Idle_Up");
        }
        else if (currentAnimation == "Walk_Down" && movement == new Vector2(0, 0))
        {
            PlayAnimation("Idle_Down");
        }

    }

    private string currentAnimation = "Idle_Down";
    private void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
        currentAnimation = animationName;
    }

}
