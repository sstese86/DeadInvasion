using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.Game;

namespace NaviEnt
{
    public interface IPoolUse
    {
        void ReturnToPool();
    }

    public class PoolManager : MonoBehaviour
    {
        [SerializeField]
        int _defaultPoolSize = 5;

        public static PoolManager Instance { get; private set; }

        GameObject _hitCollider = null;
        GameObject _effect = null;
        GameObject _emotion = null;

        Transform poolItem = null;

        public Transform HitColliderPool { get => _hitCollider.transform; }
        public Transform EffectPool { get => _effect.transform; }
        public Transform EmotionPool { get => _emotion.transform; }

        void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            InitializePool();

        }

        void InitializePool()
        {
            
            _hitCollider = new GameObject("HitCollider");
            _hitCollider.transform.parent = transform;
            
            _effect = new GameObject("Effect");
            _effect.transform.parent = transform;
            
            _emotion = new GameObject("Emotion");
            _emotion.transform.parent = transform;

        }

        public void SpawnHitCollider(Team team, int damage, Transform trans, GameObject hitCollider, NaviEntEffect hitEffect, List<AudioClipSetup> hitAudioClips)
        {
            Transform pool = null;
            pool = _hitCollider.transform.Find("["+ hitCollider.name + "]");
            if(!pool)
            {
                pool = CreateNewPoolItemRoot(HitColliderPool, hitCollider);
                AppendNewPoolItem(pool, hitCollider);
                SpawnHitCollider(team, damage, trans, hitCollider, hitEffect, hitAudioClips);
                return;
            }
            else
            {
                if(pool.childCount == 0)
                {
                    AppendNewPoolItem(pool, hitCollider);
                }
                poolItem = pool.GetChild(0);
                poolItem.parent = null;
                poolItem.position = trans.position;
                poolItem.rotation = trans.rotation;                
                poolItem.GetComponent<HitCollider>().InitHitCollider(team, damage,hitEffect,hitAudioClips);
            }
        }
        public void SpawnHitCollider(int attackAnimIndex, Transform trans, Team team, int damage, Weapon equipedWeapon)
        {
            HitCollider hitCollider = equipedWeapon.WeaponAttackSetup[attackAnimIndex].hitCollider;
            NaviEntEffect hitEffect = equipedWeapon.WeaponAttackSetup[attackAnimIndex].hitEffect;
            List<AudioClipSetup> hitSoundClips = equipedWeapon.ItemSoundClip.GetHitAudioClips;

            Transform pool = null;
            pool = _hitCollider.transform.Find("[" + hitCollider.name + "]");
            if (!pool)
            {
                pool = CreateNewPoolItemRoot(HitColliderPool, hitCollider.gameObject);
                AppendNewPoolItem(pool, hitCollider.gameObject);
                SpawnHitCollider(attackAnimIndex, trans, team, damage, equipedWeapon);
                return;
            }
            else
            {
                if (pool.childCount == 0)
                {
                    AppendNewPoolItem(pool, hitCollider.gameObject);
                }
                poolItem = pool.GetChild(0);
                poolItem.parent = null;
                poolItem.position = trans.position;
                poolItem.rotation = trans.rotation;
                poolItem.GetComponent<HitCollider>().InitHitCollider(team, damage, hitEffect, hitSoundClips);
            }
        }


        public void SpawnEffect(GameObject effectObject, Transform trans, float delay = 0f)
        {
            Transform pool = null;
            pool = _effect.transform.Find("[" + effectObject.name + "]");
            if (!pool)
            {
                pool = CreateNewPoolItemRoot(EffectPool, effectObject);
                AppendNewPoolItem(pool, effectObject);
                SpawnEffect(effectObject, trans, delay);
            }
            else
            {
                if (pool.childCount == 0)
                {
                    AppendNewPoolItem(pool, effectObject);
                }
                poolItem = pool.GetChild(0);
                poolItem.parent = null;
                poolItem.position = trans.position;
                poolItem.rotation = trans.rotation;

                NaviEntEffect effect = poolItem.GetComponent<NaviEntEffect>();
                effect.PlayEffect(delay);
            }
        }

        public void EffectBackToPoolSystem(GameObject obj)
        {
            Transform pool = _effect.transform.Find("[" + obj.name + "]");
            if (pool)
                obj.transform.parent = pool;

        }

        Transform CreateNewPoolItemRoot(Transform poolType, GameObject newPoolItem)
        {
            Transform pool = new GameObject("[" + newPoolItem.name + "]").transform;
            pool.parent = poolType.transform;
            return pool;
        }

        private void AppendNewPoolItem(Transform root, GameObject newPoolItem)
        {
            for (int i = 0; i < _defaultPoolSize-1; i++)
            {
                GameObject item = Instantiate(newPoolItem, root.transform);
                item.name = newPoolItem.name;
            }
        }

        public void HitColliderBackToPoolSystem(GameObject obj)
        {
            Transform pool = _hitCollider.transform.Find("[" + obj.name + "]");
            if (pool)
                obj.transform.parent = pool;
            
        }


    }
}