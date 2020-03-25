using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NaviEnt.Game
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        float _agroRadius = 5f;

        Transform _target = null;
        NavMeshAgent _agent = null;

        CharacterHandler _characterHandler = null;
        bool _isDead = false;
        // Start is called before the first frame update
        void Start()
        {
            _characterHandler = GetComponent<CharacterHandler>();
            _agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            isDead();
        }


        void isDead()
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



        public void OnAgroTriggerEnter(Transform target)
        {
            _target = target;
            Debug.Log(gameObject.name + "has provoked to" + _target.name);

            _agent.SetDestination(target.position);

        }

        public void OnAgroTriggerExit()
        {
            Debug.Log(gameObject.name + "has lost interest at" + _target.name);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _agroRadius);
        }
    }
}