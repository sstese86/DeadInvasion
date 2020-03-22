using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt.Game
{
    public class HitColliderHelper : MonoBehaviour
    {
        HitCollider _hitCollider = null;

        public void InitHitColliderHelper(HitCollider hitcollider)
        {
            _hitCollider = hitcollider;
        }

        void OnTriggerEnter(Collider other)
        {
            _hitCollider?.TargetEnter(other);
        }
    }
}