using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt
{
    public class Effect : MonoBehaviour, IPoolUse
    {
        float _delay = 0f;


        private void OnEnable()
        {
            _delay = GetComponent<ParticleSystem>().main.duration + 0.1f;
            StartCoroutine(ReturnToPoolRoutine());
        }

        public void PlayEffect()
        {
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
        public void PlayEffect(float delay = 0f)
        {
            gameObject.SetActive(false);

            var main = GetComponent<ParticleSystem>().main;
            main.startDelay = delay;
            
            gameObject.SetActive(true);
        }

        public void ReturnToPool()
        {
            PoolManager.Instance.EffectBackToPoolSystem(gameObject);
        }

        IEnumerator ReturnToPoolRoutine()
        {
            yield return new WaitForSeconds(_delay);
            ReturnToPool();
        }


    }
}