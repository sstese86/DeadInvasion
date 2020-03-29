using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NaviEnt.Game
{
    public class AIController : ActorController, ICanAttack
    {


        //[SerializeField]
        //float _agroRadius = 5f;
        [SerializeField]
        NavMeshAgent _agent = null;

        [SerializeField]
        float AIUpdateTickTime = 0.25f;

        Transform _root = null;

        AIActor _actor = null;
        PlayerActor _characterHandler = null;
        AIAnimatorHandler _animatorHandler = null;

        bool _isDead = false;
        bool _canUpdate = true;
        bool _isBusy = false;

        // Start is called before the first frame update
        void Start()
        {
            _actor = GetComponent<AIActor>();
            _characterHandler = GetComponent<PlayerActor>();
            _root = transform.Find("Root");
        }

        // Update is called once per frame
        void Update()
        {
            if(_canUpdate)
            {
                StartCoroutine(UpdataAIBehaviourRoutine());
            }            
            IsDead();
        }

        void MoveToTarget()
        {
            if(_target != null)
            {
                
                _agent.SetDestination(_target.position);
                _animatorHandler.UpdateAnimParmMovement(true);
                if(_canUpdateLookAtDir)
                {
                    StartCoroutine(LookAtToTargetRoutine(_root, _target));
                }
            }
        }

        void IdleState()
        {
            _target = null;
            _agent.isStopped = true;
            _animatorHandler.UpdateAnimParmMovement(false);
        }

        void IsDead()
        {
            if(_isDead)
            {
                gameObject.SetActive(false);
            }

            if (_characterHandler.isDead)
            {
                _isDead = true;
            }
        }


        public void Attack()
        {
            if(!_isBusy)
            {
                _characterHandler.ActorSoundClip.PlaySoundAttack();
                _animatorHandler.PlayAIAnimAttack1();
                _isBusy = true;
            }
        }

        void Wandering()
        {

        }

        IEnumerator UpdataAIBehaviourRoutine()
        {
            _canUpdate = false;
            MoveToTarget();
            yield return new WaitForSeconds(AIUpdateTickTime);
            _canUpdate = true;
        }

        public void OnAttackTriggerStay(Team team)
        {
            if(team != _characterHandler.DamageableTeam)
                Attack();
        }


        public void SetAIAnimatorHandler(AIAnimatorHandler handler)
        {
            _animatorHandler = handler;
        }

        public void OnAgroTriggerEnter(Transform target)
        {
            _target = target;
            _agent.isStopped = false;
        }

        public void OnAgroTriggerExit()
        {
            IdleState();
        }

        public void AnimEventAttackCallback()
        {
            //TODO AI Attack has problem unknown. Player takes damage just first time.
            PoolManager.Instance.SpawnHitCollider(_actor.GetAttack1HitCollider, _root, _actor.DamageableTeam, 2);
        }

        private void OnDrawGizmos()
        {
            //Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(transform.position, _agroRadius);
        }


    }
}