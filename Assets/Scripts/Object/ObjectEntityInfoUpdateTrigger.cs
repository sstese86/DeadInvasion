using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt.Game
{
    public class ObjectEntityInfoUpdateTrigger : MonoBehaviour
    {


        private void OnTriggerEnter(Collider other)
        {
            Actor actor = other.GetComponent<Actor>();
            if(actor != null)
            {
                if(actor.DamageableTeam == Team.Player)
                    UpdatePickupItemEntityInfo();
            }
        }

        private void UpdatePickupItemEntityInfo()
        {
            GameEventManager.Instance.OnSelectedEntityChangedCallback(transform.parent.GetComponent<PickupItem>().GetComponent<IEntity>(),transform);
        }

    }
}