using UnityEngine;
using System.Collections;

namespace NaviEnt.Game
{
    public class AIActor : Actor, IDamageable
    {
        [SerializeField]
        GameObject _attack1HitCollider = null;
        public GameObject GetAttack1HitCollider { get => _attack1HitCollider; }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}