using UnityEngine;
using System.Collections;

namespace NaviEnt.Game
{
    public class AIActor : Actor, IDamageable
    {
        [Space]
        [Header("AI Setup")]
        [SerializeField]
        GameObject _attack1HitCollider = null;
        [SerializeField]
        GameObject _attack2HitCollider = null;
        
        [SerializeField]
        NaviEntEffect _attack1HitEffect = null;
        [SerializeField]
        NaviEntEffect _attack2HitEffect = null;
        
        [SerializeField]
        FeedbackHandler _feedbackHandler = null;

        [SerializeField]
        AIActorSoundClip _aiActorSoundClip = null;

        public GameObject GetAttack1HitCollider { get => _attack1HitCollider; }
        public GameObject GetAttack2HitCollider { get => _attack2HitCollider; }
        public NaviEntEffect GetAttack1HitEffect { get => _attack1HitEffect; }
        public NaviEntEffect GetAttack2HitEffect { get => _attack2HitEffect; }

        public AIActorSoundClip GetAIActorSoundClip { get => _aiActorSoundClip; }

        // Use this for initialization
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {

        }

        protected override void DeadFeedback()
        {
            base.DeadFeedback();
            GetAIActorSoundClip.PlaySoundDead();
        }
        protected override void TakeDamageFeedback()
        {
            base.TakeDamageFeedback();
            GetAIActorSoundClip.PlaySoundHit();
            _feedbackHandler?.HitFeedback();
        }
    }
}