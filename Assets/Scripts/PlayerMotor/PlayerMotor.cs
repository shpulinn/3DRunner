using System;
using UnityEngine;
public class PlayerMotor : MonoBehaviour
{
    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public float verticalVelocity;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public int currentLane;

    // maybe make it private with serialize fields
    public float distanceInBetweenLanes = 3.0f;
    public float baseRunSpeed = 5.0f;
    public float baseSidewaySpeed = 10.0f;
    public float gravity = 14.0f;
    public float terminalVelocity = 20.0f;
    public string deathLayerName;

    public CharacterController Controller;
    public Animator animator;
    
    private BaseState _state;
    private bool _isPaused;

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        _state = GetComponent<RunningState>();
        _state.Construct();

        _isPaused = true;
    }

    private void Update()
    {
        if (_isPaused)
            return;
        UpdateMotor();
    }

    private void UpdateMotor()
    {
        // Check if player is grounded
        isGrounded = Controller.isGrounded;
        
        // How should player be moving now? Based on STATE
        moveVector = _state.ProcessMotion();
        
        // Are we changing state?
        _state.Transition();
        
        // Feed animator with values
        animator?.SetBool("IsGrounded", isGrounded);
        animator?.SetFloat("Speed", Mathf.Abs(moveVector.z));
        
        // Move player
        Controller.Move(moveVector * Time.deltaTime);
    }

    public float SnapToLane()
    {
        float r = 0.0f;

        // If player is not directly on top of the lane
        if (transform.position.x != (currentLane * distanceInBetweenLanes))
        {
            float deltaToDesiredPosition = (currentLane * distanceInBetweenLanes) - transform.position.x;
            r = (deltaToDesiredPosition > 0) ? 1 : -1;
            r *= baseSidewaySpeed;

            float actualDistance = r * Time.deltaTime; // how far player move at current frame
            if (Mathf.Abs(actualDistance) > Mathf.Abs(deltaToDesiredPosition))
            {
                r = deltaToDesiredPosition * (1 / Time.deltaTime);
            }
        }
        else
        {
            r = 0.0f;
        }
        
        return r;
    }

    public void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
    }

    public void ChangeState(BaseState state)
    {
        _state.Destruct();
        _state = state;
        _state.Construct();
    }

    public void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        if (verticalVelocity < -terminalVelocity)
        {
            verticalVelocity = -terminalVelocity;
        }
    }
    
    // Pausing player
    public void PausePlayer()
    {
        _isPaused = true;
    }

    public void ResumePlayer()
    {
        _isPaused = false;
    }

    public void RespawnPlayer()
    {
        ChangeState(GetComponent<RespawnState>());
        GameManager.Instance.ChangeCamera(GameCamera.Respawn);
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (deathLayerName == LayerMask.LayerToName(hit.gameObject.layer))
        {
            ChangeState(GetComponent<DeathState>());
        }
    }

    public void ResetPlayer()
    {
        transform.position = Vector3.zero;
        animator?.SetTrigger("Idle");
        ChangeState(GetComponent<RunningState>());
        PausePlayer();
    }
}
