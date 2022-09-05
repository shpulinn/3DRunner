using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class InputManager : MonoBehaviour
    {
        // Singletone
        private static InputManager _instance;
        public static InputManager Instance => _instance;
        
        // Action schemes
        private RunnerInputAction _inputActionScheme;
        
        // Config
        [SerializeField] private float sqrSwipeDeadzone = 50.0f;
        
        #region public properties

        public bool Tap => _tap;
        public Vector2 TouchPosition => _touchPosition;
        public bool SwipeLeft => _swipeLeft;
        public bool SwipeRight => _swipeRight;
        public bool SwipeUp => _swipeUp;
        public bool SwipeDown => _swipeDown;
        
        #endregion
        
        #region privates

        private bool _tap;
        private Vector2 _touchPosition;
        private Vector2 _startDrag;
        private bool _swipeLeft;
        private bool _swipeRight;
        private bool _swipeUp;
        private bool _swipeDown;
        
        #endregion

        private void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            
            SetupControls();
        }

        private void LateUpdate()
        {
            ResetInputs();
        }

        private void ResetInputs()
        {
            _tap = _swipeLeft = _swipeRight = _swipeUp = _swipeDown = false;
        }

        private void SetupControls()
        {
            _inputActionScheme = new RunnerInputAction();
            
            // Register different actions
            _inputActionScheme.Gameplay.Tap.performed += ctx => OnTap(ctx);
            _inputActionScheme.Gameplay.TouchPosition.performed += ctx => OnPosition(ctx);
            _inputActionScheme.Gameplay.StartDrag.performed += ctx => OnStartDrag(ctx);
            _inputActionScheme.Gameplay.EndDrag.performed += ctx => OnEndDrag(ctx);
        }

        private void OnTap(InputAction.CallbackContext ctx)
        {
            _tap = true;
        }

        private void OnPosition(InputAction.CallbackContext ctx)
        {
            _touchPosition = ctx.ReadValue<Vector2>();
        }

        private void OnStartDrag(InputAction.CallbackContext ctx)
        {
            _startDrag = _touchPosition;
        }

        private void OnEndDrag(InputAction.CallbackContext ctx)
        {
            Vector2 delta = _touchPosition - _startDrag;
            float sqrDistance = delta.sqrMagnitude;

            if (sqrDistance > sqrSwipeDeadzone) // Swipe confirmed
            {
                float x = Mathf.Abs(delta.x);
                float y = Mathf.Abs(delta.y);

                if (x > y) // Left or Right swipe
                {
                    if (delta.x > 0) // Going right
                    {
                        _swipeRight = true;
                    }
                    else // Going left
                    {
                        _swipeLeft = true;
                    }
                }
                else // Up or Down swipe
                {
                    if (delta.y > 0) // Going Up
                    {
                        _swipeUp = true;
                    }
                    else // Going down
                    {
                        _swipeDown = true;
                    }
                }
            }

            _startDrag = Vector2.zero; // reset
        }

        public void OnEnable()
        {
            _inputActionScheme.Enable();
        }

        public void OnDisable()
        {
            _inputActionScheme.Disable();
        }
    }
}
