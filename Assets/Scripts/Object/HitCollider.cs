using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NaviEnt.Game
{
    public class HitCollider : MonoBehaviour, IPoolUse
    {
        [SerializeField]
        Team _team = Team.Player;
        [SerializeField]
        int _damage = 0;

        [SerializeField]
        bool _hitMultipleTarget = false;

        [SerializeField]
        bool _debugMode = false;
        [SerializeField]
        HitColliderTrigger _trigger = null;

        NaviEntEffect _hitEffect = null;
        Transform _hitTarget = null;
        ItemSoundClip _itemSoundClip = null;

        public void InitHitCollider(Team team, int damage)
        {
            _team = team;
            _damage = damage;
            _trigger.GetComponent<Collider>().enabled = true;
            _hitEffect = null;
            if (_debugMode)
            {
                _trigger.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                _trigger.GetComponent<MeshRenderer>().enabled = false;
            }
            StartCoroutine(ReturnToPoolRoutine());
        }

        public void InitHitCollider(Team team, int damage, NaviEntEffect hitEffect, ItemSoundClip itemSoundClip)
        {
            _team = team;
            _damage = damage;
            _hitEffect = hitEffect;
            _itemSoundClip = itemSoundClip;

            _trigger.GetComponent<Collider>().enabled = true;
            if (_debugMode)
            {
                _trigger.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                _trigger.GetComponent<MeshRenderer>().enabled = false;
            }
            StartCoroutine(ReturnToPoolRoutine());
        }
        // Start is called before the first frame update
        void Start()
        {
            _trigger.InitHitColliderHelper(this);
            if (_debugMode)
            {
                _trigger.GetComponent<MeshRenderer>().enabled = true;
            }
            else
                _trigger.GetComponent<MeshRenderer>().enabled = false;
        }

        public void TargetEnter(Collider other)
        {
            IDamageable target = other.GetComponent<IDamageable>();
            if (target == null) return;

            if(_hitMultipleTarget)
            {
                if (target.TakeDamage(_team, _damage))
                {
                    ApplyDamageFeedback(other);
                }

            }
            else
            {
                if (target.TakeDamage(_team, _damage))
                {
                    ApplyDamageFeedback(other);
                    ReturnToPool();
                }
            }            
        }

        void ApplyDamageFeedback(Collider other)
        {
            Actor actor = other.GetComponent<Actor>();
            if (actor == null) return;
            if (_hitEffect != null)
                {
                    _hitTarget = actor.GetComponent<ActorSocketFinder>().Body;
                    if (_hitTarget == null)
                    {
                        _hitTarget = actor.transform;
                    }
                    PoolManager.Instance.SpawnEffect(_hitEffect.gameObject, _hitTarget);

            }
            if (_itemSoundClip != null)
                _itemSoundClip.PlaySoundItemHitActor();
        }
        
        public void ReturnToPool()
        {
            _trigger.GetComponent<Collider>().enabled = false;
            if (_debugMode)
            {
                _trigger.GetComponent<MeshRenderer>().enabled = false;
            }
            PoolManager.Instance.HitColliderBackToPoolSystem(gameObject);
        }

        IEnumerator ReturnToPoolRoutine()
        {
            yield return new WaitForSeconds(0.1f);
            ReturnToPool();
        }
    }
}
