using System;
using UnityEngine;

namespace PlayerMotor
{
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

        public CharacterController Controller;

        private void Start()
        {
            Controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            UpdateMotor();
        }

        private void UpdateMotor()
        {
            // Check if player is grounded
            isGrounded = Controller.isGrounded;
            
            // How should player be moving now? Based on STATE
            
            // Are we changing state?
            
            // Move player
            Controller.Move(moveVector * Time.deltaTime);
        }
    }
}
