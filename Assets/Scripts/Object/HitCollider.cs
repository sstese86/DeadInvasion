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
        HitColliderHelper helper = null;

        GameObject _hitEffect = null;
        Transform _hitTarget = null;

        public void InitHitCollider(Team team, int damage)
        {
            this._team = team;
            this._damage = damage;
            helper.GetComponent<Collider>().enabled = true;
            _hitEffect = null;
            if (_debugMode)
            {
                helper.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                helper.GetComponent<MeshRenderer>().enabled = false;
            }
            StartCoroutine(ReturnToPoolRoutine());
        }

        public void InitHitCollider(GameObject hitEffect, Team team, int damage)
        {
            this._team = team;
            this._damage = damage;
            helper.GetComponent<Collider>().enabled = true;
            _hitEffect = hitEffect;
            if (_debugMode)
            {
                helper.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                helper.GetComponent<MeshRenderer>().enabled = false;
            }
            StartCoroutine(ReturnToPoolRoutine());
        }
        // Start is called before the first frame update
        void Start()
        {
            helper.InitHitColliderHelper(this);
            if (_debugMode)
            {
                helper.GetComponent<MeshRenderer>().enabled = true;
                helper.GetComponent<Collider>().enabled = true;
            }
            else
                helper.GetComponent<MeshRenderer>().enabled = false;
        }

        public void TargetEnter(Collider other)
        {
            IDamageable target = other.GetComponent<IDamageable>();
            if(target.TakeDamage(_team, _damage))
            {
                if (_hitEffect != null)
                {                    
                    _hitTarget = other.GetComponent<ActorSocketFinder>().Body;
                    if(_hitTarget == null)
                    {
                        _hitTarget = other.transform;
                    }
                    PoolManager.Instance.SpawnEffect(_hitEffect, _hitTarget);
                }
            }                
            ReturnToPool();
        }

        public void ReturnToPool()
        {
            helper.GetComponent<Collider>().enabled = false;
            if (_debugMode)
            {
                helper.GetComponent<MeshRenderer>().enabled = false;
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
