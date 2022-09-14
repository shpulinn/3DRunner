using Inputs;
using UnityEngine;

public class RespawnState : BaseState
{
    [SerializeField] private float verticalDistance = 25.0f;
    [SerializeField] private float immunityTime = 1.0f;

    private float _startTime; 
    
    public override void Construct()
    {
        _startTime = Time.time;
        
        playerMotor.Controller.enabled = false;
        playerMotor.transform.position = new Vector3(0, verticalDistance, playerMotor.transform.position.z);
        playerMotor.Controller.enabled = true;

        playerMotor.verticalVelocity = 0.0f;
        playerMotor.ChangeLane(0);
        playerMotor.animator?.SetTrigger("Respawn");
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
        if (playerMotor.isGrounded && Time.time - _startTime > immunityTime)
        {
            playerMotor.ChangeState(GetComponent<RunningState>());
        }
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
    }

    public override void Destruct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Game);
    }
}
