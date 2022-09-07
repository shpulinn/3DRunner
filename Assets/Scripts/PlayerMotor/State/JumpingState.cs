using Inputs;
using UnityEngine;

public class JumpingState : BaseState
{
    public float jumpForce = 7.0f;
    
    public override void Construct()
    {
        playerMotor.verticalVelocity = jumpForce;
        playerMotor.animator?.SetTrigger("Jump");
    }

    public override Vector3 ProcessMotion()
    {
        // Apply gravity
        playerMotor.ApplyGravity();

        // Create return vector
        Vector3 move = Vector3.zero;

        move.x = playerMotor.SnapToLane();
        move.y = playerMotor.verticalVelocity;
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
        if (playerMotor.verticalVelocity < 0)
        {
            playerMotor.ChangeState(GetComponent<FallingState>());
        }
    }
}
