using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace NaviEnt.Game
{
    public class PlayerController : ActorController
    {

        PlayerActor _actor = null;
        CharacterController _characterController = null;
        InputManager _inputManager = null;
        PlayerAnimatorHandler _animatorHandler = null;

        Vector3 _moveDirectionVector = Vector3.zero;
        Vector3 _jumpVector = Vector3.zero;

        [SerializeField]
        Transform _camTargetLookAt = null;
        [SerializeField]
        Transform _camTargetFollow = null;
        
        public Transform GetCamTargetLookAt { get => _camTargetLookAt; }
        public Transform GetCamTargetFollow { get => _camTargetFollow; }

        float _gravity = 1.5f;
        float _currentAttackAnimIndex = 0f;
        float targetSearchRange = 4f;

        public bool IsInputEnable { get; set; }

        protected override void Start()
        {
            base.Start();

            _inputManager = InputManager.Instance;
            _characterController = GetComponent<CharacterController>();
            _actor = GetComponent<PlayerActor>();

            _gravity = GameManager.Instance.gravity;

            IsBusy = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (_isDead) return;
            if (_actor.IsDead)
            {
                _isDead = true;
                PlayerDead();
                return; 
            }
            Rotate();
            InputActionCheck();
            AnimatorParmUpdate();
            CheckLookAtToTarget();
        }

        void FixedUpdate()
        {
            if (_isDead) return;  
            if(IsInputEnable)
            {
                Movement();
                MoveInAir();
            }
        }

        public void Jump()
        {
            if(_characterController.isGrounded && !IsBusy)
            {
                if (_animatorHandler != null)
                    IsBusy = true;
                    if (!_animatorHandler.Jump()) return;

                //_characterController.height = _characterHeight / 2f;
                //Vector3 center = new Vector3(0f, _characterCenterY / 2f, 0f);
                //_characterController.center = center;
                _actor.GetActorSoundClip?.PlaySoundJump();
                _jumpVector.y = _actor.ModifiedState.jumpSpeed;
                
            }           
        }
        public void Movement()
        {           
            if (_inputManager.MovementAxis == Vector2.zero) return;
            Vector3 Movement = _moveDirectionVector * _actor.ModifiedState.moveSpeed;
            _characterController.SimpleMove(Movement);
        }

        void MoveInAir()
        {
            _jumpVector.y -= _gravity * Time.deltaTime;
            _jumpVector.y = Mathf.Clamp(_jumpVector.y, -_gravity * 2, _gravity * 2);
            //if(_characterController.isGrounded)
            //{
            //    _jumpVector.y = 0f;
            //}
            _characterController.Move(_jumpVector);
        }
               
        void Rotate()
        {
            if(_canInputRotate)
            {
                if (_inputManager.MovementAxis == Vector2.zero) return;

                Vector3 Axis = _inputManager.MovementAxis;
                Quaternion rotateFrom = _root.rotation;
                Vector3 direction;
                Quaternion rotateTo = new Quaternion();

                float moveSpeed = _actor.ModifiedState.moveSpeed * 0.65f;
                float rotationDuration = moveSpeed * Time.deltaTime * 2f;
                direction = new Vector3(Axis.x, 0, Axis.y);

                rotateTo = Quaternion.LookRotation(direction, Vector3.up);
                _root.rotation = Quaternion.Slerp(rotateFrom, rotateTo, rotationDuration);
            }

        }
        
        // Later Make it Iterator To Look at To Target then Invoke Attack to target
        void CheckLookAtToTarget()
        {
            if (_canUpdateLookAtDir)
            {
                if (_isCombatMode && _target != null)
                {
                    StartCoroutine(LookAtToTargetRoutine(_root, _target));
                }
            }           
        }

        void InputActionCheck()
        {
            Vector3 direction = new Vector3(_inputManager.MovementAxis.x, 0, _inputManager.MovementAxis.y).normalized;
            _moveDirectionVector = new Vector3(_inputManager.MovementAxis.x* Mathf.Abs(direction.x),
                                                0, 
                                                _inputManager.MovementAxis.y * Mathf.Abs(direction.z));

            if (_inputManager.Fire1Input) Attack();
            if(_inputManager.JumpInput) Jump();
        }

        private void PlayerDead()
        {
            StopAllCoroutines();
            GameEventManager.Instance.OnPlayerDead();
            _animatorHandler.Dead();
        }

        void AnimatorParmUpdate()
        {
            if(_animatorHandler != null)
            {
                _animatorHandler.UpdateAnimParmMoveSpeed(_moveDirectionVector.sqrMagnitude);
            }
        }

        public void Attack()
        {
            
            if (!IsBusy)
            {
                SerchForTarget();

                _currentAttackAnimIndex = _actor.GetAttackAnimIndex;
                _actor.GetActorSoundClip?.PlaySoundAttack();
                _actor.GetWeaponSoundClip?.PlaySoundItemAttack();
                _animatorHandler.Attack(_actor.GetWeaponType, _currentAttackAnimIndex);
                AttackCooldown = _actor.GetWeaponFireRate;
                IsBusy = true;
                _isCombatMode = true;
            }   
        }

        public void SpawnCallback()
        {
            StartCoroutine(SpawnCallbackRoutine());
        }

        IEnumerator SpawnCallbackRoutine()
        {
            yield return null;
            IsInputEnable = true;
        }

        public void AnimEventAttackCallback()
        {
           SpawnHitCollider();
            _target = null;
        }
        public void AnimEventJumpEndCallback()
        {
            //_characterController.height = _characterHeight;
            //Vector3 center = new Vector3(0f, _characterCenterY, 0f);
            //_characterController.center = center;
        }

        void SpawnHitCollider()
        {
            if(_actor.GetCurrentWeapon != null)
            {
                PoolManager.Instance.SpawnHitCollider( (int)_currentAttackAnimIndex, _root, _actor.DamageableTeam, _actor.Damage, _actor.GetCurrentWeapon);
            }
        }


        void SerchForTarget()
        {
            targetSearchRange = _actor.GetWeaponRange + 1f;

            Transform target = null;
            float minDistance = Mathf.Infinity;
            Collider[] colliders = Physics.OverlapSphere(transform.position, targetSearchRange);
            foreach (Collider col in colliders)
            {

                Actor obj = col.GetComponent<Actor>();
                if(obj != null && obj.DamageableTeam != Team.Player)
                {
                    float distance = (transform.position - col.transform.position).sqrMagnitude;
                    if(distance < minDistance)
                    {
                        minDistance = distance;
                        target = col.transform;
                    }
                }
            }

            if(target != null)
            {
                _target = target;
                GameEventManager.Instance.OnSelectedEntityChangedCallback(_target.GetComponent<IEntity>(), _target);
            }
            else
            {
                _target = null;
            }
            
        }
        public void SetCharacterAnimatorHandler(PlayerAnimatorHandler animHandler)
        {
            _animatorHandler = animHandler;
        }


        public void NotCombatMode()
        {
            _isCombatMode = false;
        }

        public void NotBusy()
        {
            StopCoroutine(NotBusyRoutine());
            StartCoroutine(NotBusyRoutine(AttackCooldown));
        }
        public void NotBusy(float delay)
        {
            StopCoroutine(NotBusyRoutine());
            StartCoroutine(NotBusyRoutine(delay));
        }

        IEnumerator NotBusyRoutine(float delay = 0f)
        {
            yield return new WaitForSeconds(delay);
            IsBusy = false;
        }

    }
}