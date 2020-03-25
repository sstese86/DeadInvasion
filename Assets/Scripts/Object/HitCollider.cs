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
        bool _debugMode = false;
        [SerializeField]
        HitColliderHelper _helper = null;

        NaviEntEffect _hitEffect = null;
        Transform _hitTarget = null;
        ItemSoundClip _itemSoundClip = null;

        public void InitHitCollider(Team team, int damage)
        {
            this._team = team;
            this._damage = damage;
            _helper.GetComponent<Collider>().enabled = true;
            _hitEffect = null;
            if (_debugMode)
            {
                _helper.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                _helper.GetComponent<MeshRenderer>().enabled = false;
            }
            StartCoroutine(ReturnToPoolRoutine());
        }

        public void InitHitCollider(Team team, int damage, NaviEntEffect hitEffect, ItemSoundClip itemSoundClip)
        {
            _team = team;
            _damage = damage;
            _hitEffect = hitEffect;
            _itemSoundClip = itemSoundClip;

            _helper.GetComponent<Collider>().enabled = true;
            if (_debugMode)
            {
                _helper.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                _helper.GetComponent<MeshRenderer>().enabled = false;
            }
            StartCoroutine(ReturnToPoolRoutine());
        }
        // Start is called before the first frame update
        void Start()
        {
            _helper.InitHitColliderHelper(this);
            if (_debugMode)
            {
                _helper.GetComponent<MeshRenderer>().enabled = true;
                _helper.GetComponent<Collider>().enabled = true;
            }
            else
                _helper.GetComponent<MeshRenderer>().enabled = false;
        }

        public void TargetEnter(Collider other)
        {
            IDamageable target = other.GetComponent<IDamageable>();
            if (target == null) return;
            if(target.TakeDamage(_team, _damage))
            {
                if (_hitEffect != null)
                {                    
                    _hitTarget = other.GetComponent<ActorSocketFinder>().Body;
                    if(_hitTarget == null)
                    {
                        _hitTarget = other.transform;
                    }
                    PoolManager.Instance.SpawnEffect(_hitEffect.gameObject, _hitTarget);
                    _itemSoundClip.PlaySoundItemHitActor();
                }
            }                
            ReturnToPool();
        }

        public void ReturnToPool()
        {
            _helper.GetComponent<Collider>().enabled = false;
            if (_debugMode)
            {
                _helper.GetComponent<MeshRenderer>().enabled = false;
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
