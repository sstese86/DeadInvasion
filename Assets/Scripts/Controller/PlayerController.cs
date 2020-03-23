using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace NaviEnt.Game
{
    public class PlayerController : MonoBehaviour, ICanMove, ICanAttack
    {

        CharacterHandler _characterHandler = null;
        CharacterController _characterController = null;
        InputManager _inputManager = null;

        Transform _root = null;
        Transform _target = null;
        CharacterAnimatorHandler _characterAnimatorHandler = null;

        Vector3 _moveDirectionVector = Vector3.zero;
        Vector3 _jumpVector = Vector3.zero;

        bool isCombatMode = false;
        
        float _gravity = 1.5f;

        float _characterHeight = 0f;
        float _characterCenterY = 0f;

        bool _isDead = false;
        public bool IsBusy { get; set; }

        public float targetSearchRange = 5f;

        public int testWeaponIndex = 0;
        public float testAttackAnimIndex = 0f;
        public GameObject testHitCollider = null;
        public GameObject testParticlePrefab = null;

        // Start is called before the first frame update
        void Start()
        {
            _inputManager = InputManager.Instance;
            _characterController = GetComponent<CharacterController>();
            _characterHandler = GetComponent<CharacterHandler>();

            _root = transform.Find("Root");                

            _gravity = GameManager.Instance.gravity;

            _characterHeight = _characterController.height;
            _characterCenterY = _characterController.center.y;
            IsBusy = false;
        }

        private void OnDestroy()
        {
            
        }

        private void PlayerDead()
        {
            GameEventManager.Instance.OnPlayerDead();
            _characterAnimatorHandler.Dead();
        }

        // Update is called once per frame
        void Update()
        {
            if (_isDead) return;
            if (_characterHandler.isDead)
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
            Move();
            MoveInAir();
        }
        public void Jump()
        {
            if(_characterController.isGrounded && !IsBusy)
            {
                if (_characterAnimatorHandler != null)
                    IsBusy = true;
                    if (!_characterAnimatorHandler.Jump()) return;

                //_characterController.height = _characterHeight / 2f;
                //Vector3 center = new Vector3(0f, _characterCenterY / 2f, 0f);
                //_characterController.center = center;
                _characterHandler.ActorSoundClip?.PlaySoundJump();
                _jumpVector.y = _characterHandler.ModifiedState.jumpSpeed;
                
            }           
        }
        public void Move()
        {
            if (_inputManager.MovementAxis == Vector2.zero) return;
            Vector3 Movement = _moveDirectionVector * _characterHandler.ModifiedState.moveSpeed;
            _characterController.SimpleMove(Movement);


        }
        void MoveInAir()
        {
            _jumpVector.y -= _gravity * Time.deltaTime;
            _jumpVector.y = Mathf.Clamp(_jumpVector.y, -_gravity * 2, _gravity * 2);
            _characterController.Move(_jumpVector);
        }
               
        void Rotate()
        {
            if (_inputManager.MovementAxis == Vector2.zero) return;

            Vector3 Axis = _inputManager.MovementAxis;
            Quaternion rotateFrom = _root.rotation;
            Vector3 direction;
            Quaternion rotateTo = new Quaternion();
            
            float moveSpeed = _characterHandler.ModifiedState.moveSpeed * 0.65f;
            float rotationDuration = moveSpeed * Time.deltaTime * 2f;
            direction = new Vector3(Axis.x, 0, Axis.y);

            rotateTo = Quaternion.LookRotation(direction, Vector3.up);
            _root.rotation = Quaternion.Slerp(rotateFrom, rotateTo, rotationDuration);
        }

        

        IEnumerator LookAtToTargetRoutine()
        {
            Quaternion rotateFrom = _root.rotation;
            //   float moveSpeed = _characterHandler.ModifiedState.moveSpeed * 0.65f;
            //   float rotationDuration = moveSpeed * Time.deltaTime * 2f;
            Vector3 direction = _target.position - transform.position;
            Quaternion rotateTo = Quaternion.LookRotation(direction, Vector3.up);

            float alpha = 0.0f;
            while(alpha < 1f)
            {
                _root.transform.rotation = Quaternion.Slerp(rotateFrom, rotateTo, alpha);
                alpha += 0.1f;
                yield return new WaitForSeconds(0.01f);
            }
        }

        // Later Make it Iterator To Look at To Target then Invoke Attack to target
        void CheckLookAtToTarget()
        {

            // Auto targetting for attack.
            if (isCombatMode && _target != null)
            {
                StopCoroutine(LookAtToTargetRoutine());
                StartCoroutine(LookAtToTargetRoutine());
            }
        }

        void InputActionCheck()
        {
            _moveDirectionVector = new Vector3(_inputManager.MovementAxis.x, 0, _inputManager.MovementAxis.y);
            if (_inputManager.Fire1Input) Attack();
            if(_inputManager.JumpInput) Jump();
        }

        public void SetCharacterAnimatorHandler(CharacterAnimatorHandler animHandler)
        {
            _characterAnimatorHandler = animHandler;
        }

        void AnimatorParmUpdate()
        {
            if(_characterAnimatorHandler != null)
            {
                _characterAnimatorHandler.UpdateAnimParmMoveSpeed(_moveDirectionVector.sqrMagnitude);
            }
        }

        public void Attack()
        {
            SerchForTarget();
            if (!IsBusy)
            {
                _characterHandler.ActorSoundClip?.PlaySoundAttack();
                _characterHandler.WeaponSoundClip?.PlaySoundItemAttack();
                _characterAnimatorHandler.Attack(_characterHandler.WeaponType, _characterHandler.AttackAnimIndex);
                IsBusy = true;
                isCombatMode = true;
            }   
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
            if(testHitCollider!= null)
            {
                PoolManager.Instance.SpawnHitCollider(testHitCollider, _root, _characterHandler.ActorTeam, _characterHandler.Damage, testParticlePrefab);
            }
        }


        void SerchForTarget()
        {
            Transform target = null;
            float minDistance = Mathf.Infinity;
            Collider[] colliders = Physics.OverlapSphere(transform.position, targetSearchRange);
            foreach (Collider col in colliders)
            {

                Actor obj = col.GetComponent<Actor>();
                if(obj != null && obj.ActorTeam != Team.Player)
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
                //GameEventManager.Instance.OnPlayer
            }
            else
            {
                _target = null;
            }
            
        }
        
        public void NotCombatMode()
        {
            isCombatMode = false;
        }

        public void NotBusy(float delay = 0f)
        {
            StopAllCoroutines();
            StartCoroutine(NotBusyRoutine(delay));
        }

        IEnumerator NotBusyRoutine(float delay = 0f)
        {
            yield return new WaitForSeconds(delay);
            IsBusy = false;
        }

    }
}