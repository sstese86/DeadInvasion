using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt.Game
{
    public class AIAttackTrigger : MonoBehaviour
    {

        AIController _aiController = null;

        private void Start()
        {
            _aiController = transform.parent.parent?.GetComponent<AIController>();
        }

        private void OnTriggerStay(Collider other)
        {
            Actor actor = other.GetComponent<Actor>();
            if(actor != null)
            {                                
                 _aiController.OnAttackTriggerStay(actor);
            }
            
        }


    }
}