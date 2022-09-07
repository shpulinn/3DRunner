using Inputs;
using UnityEngine;

public class RunningState : BaseState
{
    public override void Construct()
    {
        playerMotor.verticalVelocity = 0.0f;
    }

    public override Vector3 ProcessMotion()
    {
        Vector3 move = Vector3.zero;

        move.x = playerMotor.SnapToLane();
        move.y = -1.0f;
        move.z = playerMotor.baseRunSpeed;

        return move;
    }

    public override void Transition()
    {
        if (InputManager.Instance.SwipeLeft)
        {
            // Change lane to left (-1)
            playerMotor.ChangeLane(-1);
        }
        if (InputManager.Instance.SwipeRight)
        {
            // Change lane to right (+1)
            playerMotor.ChangeLane(1);
        }
        if (InputManager.Instance.SwipeUp && playerMotor.isGrounded)
        {
            // Change to jumping state
            playerMotor.ChangeState(GetComponent<JumpingState>());
        }
        if (InputManager.Instance.SwipeDown && playerMotor.isGrounded)
        {
            // Change to jumping state
            playerMotor.ChangeState(GetComponent<SlidingState>());
        }
        if (!playerMotor.isGrounded)
        {
            playerMotor.ChangeState(GetComponent<FallingState>());
        }
    }
}
