using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt.Game
{
    public class AIAttackTrigger : MonoBehaviour
    {

        bool _isTimerReady = true;
        AIController _aiController = null;
        private void OnTriggerStay(Collider other)
        {
            Actor actor = other.GetComponent<Actor>();
            if(actor != null)
            {
                _aiController.OnAttackTriggerStay(actor.DamageableTeam);
                //if (_isTimerReady)
                //    StartCoroutine(TriggerAttack(actor.ActorTeam));
            }
            
        }

        IEnumerator TriggerAttack(Team team)
        {
            _isTimerReady = false;
            

            yield return new WaitForSeconds(1f);

            _isTimerReady = true;
        }

        private void Start()
        {
            _aiController = transform.parent.parent?.GetComponent<AIController>();
        }
    }
}