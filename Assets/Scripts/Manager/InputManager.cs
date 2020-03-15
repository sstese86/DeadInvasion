using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt;
using System;

namespace NaviEnt
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        public Vector2 MovementAxis{ get; set; }

        public bool JumpInput { get; private set; }
        public bool Fire1Input { get; private set; }


        bool _isJumpButtonPressed = false;
        bool _isAttackButtonPressed = false;
        // Update is called once per frame
        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }

        private void Start()
        {
            GameEventManager.onPlayerAttackButtonPressed += AttackButtonPressed;
            GameEventManager.onPlayerJumpButtonPressed += JumpButtonPressed;

        }

        private void OnDestroy()
        {
            GameEventManager.onPlayerAttackButtonPressed -= AttackButtonPressed;
            GameEventManager.onPlayerJumpButtonPressed -= JumpButtonPressed;
        }


        void Update()
        {
            CheckAndroidBackButton();
            CheckJumpInput();
            CheckAttack();
        }

        void LateUpdate()
        {
            ResetInputs();
        }
        void CheckAndroidBackButton()
        {
            if(Application.platform == RuntimePlatform.Android)
            {
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit();
                }
            }
        }

        void CheckJumpInput()
        {
            if(Input.GetButton("Jump")||_isJumpButtonPressed)
            {
                JumpInput = true;
            }
        }

        void CheckAttack()
        {
            if(Input.GetButton("Fire1")|| _isAttackButtonPressed)
            {
                Fire1Input = true;
            }
        }
        
        void AttackButtonPressed()
        {
            _isAttackButtonPressed = true;
        }
        
        void JumpButtonPressed()
        {
            _isJumpButtonPressed = true;
        }

        void ResetInputs()
        {
            Fire1Input = false;
            _isAttackButtonPressed = false;

            JumpInput = false;
            _isJumpButtonPressed = false;
        }
    }
}