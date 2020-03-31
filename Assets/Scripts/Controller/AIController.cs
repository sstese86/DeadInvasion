
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NaviEnt.Game
{
    public class AIController : ActorController
    {
        [SerializeField]
        Enemy _enemy = null;
        
        //[SerializeField]
        //float _agroRadius = 5f;

        [SerializeField]
        NavMeshAgent _agent = null;

        [SerializeField]
        float _aiUpdateTickTime = 0.25f;
        
        [SerializeField]
        float _animSpeedVariation = 0f;

        Transform _root = null;
        AIActor _actor = null;
        

        AIAnimatorHandler _animatorHandler = null;

        public Team AITeam { get => _actor.DamageableTeam; }

        bool _canUpdate = true;
        

        void Start()
        {
            _actor = GetComponent<AIActor>();
            _root = transform.Find("Root");
            InitAnimSpeedVariationValue();
        }

        void Update()
        {

            if (_isDead == true) return;
            if (_actor.IsDead)
            {
                AIDead();
            }
            if (_canUpdate && _isDead == false)
            {
                StartCoroutine(UpdataAIBehaviourRoutine());
            }
        }

        void InitAnimSpeedVariationValue()
        {
           float value = Random.Range(-1f, 1f)* _animSpeedVariation;
            value = value + 1f;
            _animatorHandler.SetAnimSpeedVariation(value);
        }

        void MoveToTarget()
        {
            if(_target != null)
            {
                _agent.SetDestination(_target.position);
                _animatorHandler.UpdateAnimParmMove(true);
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
            _animatorHandler.UpdateAnimParmMove(false);

        }

        void AIDead()
        {
            _isDead = true;
            _enemy?.EnemyDeadCallback();
            StopAllCoroutines();
            _animatorHandler.PlayAIAnimDead();
            StartCoroutine(DeadWaitTimer());
        }


        public void Attack()
        {
            //At the moment just Attack 1. Later Implement Attack 2 for critical attack or somthing.
  
                _actor.GetAIActorSoundClip.PlaySoundAttack1();
                _animatorHandler.PlayAIAnimAttack1();
                IsBusy = true;

        }

        void Wandering()
        {

        }

        void ReturnToPool()
        {
            gameObject.SetActive(false);
        }

        IEnumerator DeadWaitTimer()
        {
            yield return new WaitForSeconds(5f);
            ReturnToPool();
        }

        IEnumerator UpdataAIBehaviourRoutine()
        {
            _canUpdate = false;
            MoveToTarget();
            yield return new WaitForSeconds(_aiUpdateTickTime);
            _canUpdate = true;
        }

        public void OnAttackTriggerStay(Actor actor)
        {
            if (!IsBusy)
            {
                if (actor.DamageableTeam != _actor.DamageableTeam)
                    Attack();
            }
        }


        public void SetAIAnimatorHandler(AIAnimatorHandler handler)
        {
            _animatorHandler = handler;
        }

        public void OnAggroTriggerEnter(Transform target)
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
            PoolManager.Instance.SpawnHitCollider(_actor.DamageableTeam, _actor.Damage, _root, _actor.GetAttack1HitCollider, _actor.GetAttack1HitEffect, _actor.GetAIActorSoundClip.GetAttack1HitAudioClips );
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


        private void OnDrawGizmos()
        {
            //Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(transform.position, _agroRadius);
        }


    }
}