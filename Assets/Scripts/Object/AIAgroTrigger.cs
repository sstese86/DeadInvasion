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
            CharacterHandler target = other.gameObject.GetComponent<CharacterHandler>();
            if (target == null) return;
            if (target.ActorTeam != _aiController.GetComponent<CharacterHandler>().ActorTeam)
            {
                _aiController.OnAgroTriggerEnter(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform == _target)
            {
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