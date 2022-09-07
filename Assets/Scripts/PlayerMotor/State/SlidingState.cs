using Inputs;
using UnityEngine;

public class SlidingState : BaseState
{
    public float slideDuration = 1.0f;
    
    // Collider logic
    private Vector3 _initialColliderCenter;
    private float _initialColliderSize;
    private float _slideStart; // in time

    public override void Construct()
    {
        playerMotor.animator?.SetTrigger("Slide");
        _slideStart = Time.time;
        _initialColliderSize = playerMotor.Controller.height;
        _initialColliderCenter = playerMotor.Controller.center;

        playerMotor.Controller.height = _initialColliderSize * 0.5f;
        playerMotor.Controller.center = _initialColliderCenter * 0.5f;
    }

    public override Vector3 ProcessMotion()
    {
        Vector3 move = Vector3.zero;

        move.x = playerMotor.SnapToLane();
        move.y = -1.0f;
        move.z = playerMotor.baseRunSpeed;

        return move;
    }

    public override void Destruct()
    {
        playerMotor.Controller.height = _initialColliderSize;
        playerMotor.Controller.center = _initialColliderCenter;
        playerMotor.animator?.SetTrigger("Running");
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
        if (playerMotor.isGrounded == false)
        {
            playerMotor.ChangeState(GetComponent<FallingState>());
        }

        if (InputManager.Instance.SwipeUp)
        {
            playerMotor.ChangeState(GetComponent<JumpingState>());
        }

        if (Time.time - _slideStart > slideDuration)
        {
            playerMotor.ChangeState(GetComponent<RunningState>());
        }
    }
}
