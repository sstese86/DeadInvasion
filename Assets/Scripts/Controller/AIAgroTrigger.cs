using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace NaviEnt.Game
{
    public class AIAgroTrigger : MonoBehaviour
    {
        [SerializeField]
        Transform _target = null;

        AIController _aiController = null;
        

        private void OnTriggerEnter(Collider other)
        {
            PlayerActor target = other.gameObject.GetComponent<PlayerActor>();
            if (target == null) return;
            if (target.DamageableTeam != _aiController.GetComponent<PlayerActor>().DamageableTeam)
            {
                _target = target.transform;
                _aiController.OnAgroTriggerEnter(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            
            if (other.transform == _target)
            {
                _target = null;
                _aiController.OnAgroTriggerExit();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            _aiController = transform.parent.parent?.GetComponent<AIController>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}