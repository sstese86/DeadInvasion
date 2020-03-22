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

        ActorSoundClip _actorSoundClip = null;

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

        public float testWeaponRange = 5f;
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
            _actorSoundClip = GetComponent<ActorSoundClip>();

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
                _actorSoundClip.PlaySoundJump();
                _jumpVector.y = _characterHandler.State.jumpSpeed;
                
            }           
        }
        public void Move()
        {
            if (_inputManager.MovementAxis == Vector2.zero) return;
            Vector3 Movement = _moveDirectionVector * _characterHandler.State.moveSpeed;
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
            
            float moveSpeed = _characterHandler.State.moveSpeed * 0.65f;
            float rotationDuration = moveSpeed * Time.deltaTime * 2f;
            direction = new Vector3(Axis.x, 0, Axis.y);

            rotateTo = Quaternion.LookRotation(direction, Vector3.up);
            _root.rotation = Quaternion.Slerp(rotateFrom, rotateTo, rotationDuration);
            

            // Auto targetting for attack.
            if (isCombatMode && _target != null)
            {
                LookAtToTarget(direction, rotateTo);
            }
        }

        // Later Make it Iterator To Look at To Target then Invoke Attack to target
        void LookAtToTarget(Vector3 direction, Quaternion rotateTo)
        {
            Quaternion rotateFrom = _root.rotation;
            float moveSpeed = _characterHandler.State.moveSpeed * 0.65f;
            float rotationDuration = moveSpeed * Time.deltaTime * 2f;

            direction = _target.position - transform.position;
            rotateTo = Quaternion.LookRotation(direction, Vector3.up);

            _root.transform.rotation = Quaternion.Slerp(rotateFrom, rotateTo, rotationDuration * 3f);
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
                _actorSoundClip?.PlaySoundAttack();
                _characterAnimatorHandler.Attack(testWeaponIndex, testAttackAnimIndex);
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
                PoolManager.Instance.SpawnHitCollider(testHitCollider, _root, _characterHandler.ActorTeam, _characterHandler.State.damage, testParticlePrefab);
            }
        }


        void SerchForTarget()
        {
            Transform target = null;
            float minDistance = Mathf.Infinity;
            Collider[] colliders = Physics.OverlapSphere(transform.position, testWeaponRange);
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