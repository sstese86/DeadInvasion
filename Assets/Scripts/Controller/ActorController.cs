using UnityEngine;
using System.Collections;

namespace NaviEnt.Game
{
    public class ActorController : MonoBehaviour
    {
        protected bool _canUpdateLookAtDir = true;
        protected bool _canInputRotate = true;
        protected Transform _target = null;
        protected bool _isCombatMode = false;
        protected bool _isDead = false;

        public bool IsBusy { get; set; }
        public float AttackCooldown { get; set; }

        protected IEnumerator LookAtToTargetRoutine(Transform root, Transform target)
        {
            _canUpdateLookAtDir = false;
            _canInputRotate = false;

            Quaternion rotateFrom = root.rotation;
            Vector3 targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
            Vector3 direction = targetPos - transform.position;
            Quaternion rotateTo = Quaternion.LookRotation(direction, Vector3.up);

            float alpha = 0.0f;
            while (alpha < 1f)
            {
                root.transform.rotation = Quaternion.Slerp(rotateFrom, rotateTo, alpha);
                alpha += 0.1f;
                yield return null;
            }
            _canUpdateLookAtDir = true;
            _canInputRotate = true;
        }

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