using UnityEngine;
using System.Collections;

namespace NaviEnt.Game
{
    public class AIAnimatorHandler : MonoBehaviour
    {
        AIController _aiController = null;
        Animator _animator = null;
        // Use this for initialization

        
        int _hashParmMove = 0;
        int _hashParmAttack1 = 0;
        int _hashParmAttack2 = 0;
        int _hashParmDead = 0;
        int _hashParmIsDead = 0;
        int _hashParmHit = 0;
        int _hashParmIsBusy = 0;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _aiController = transform.parent.parent.GetComponent<AIController>();
            _aiController.SetAIAnimatorHandler(this);
            GetHashNames();
        }

        public void UpdateAnimParmMove(bool isMove)
        {
            _animator.SetBool(_hashParmMove, isMove);
        }
        public void PlayAIAnimAttack1()
        {
            _animator.SetTrigger(_hashParmAttack1);
            //_animator.SetBool(_hashParmIsBusy, true);
        }
        public void PlayAIAnimAttack2()
        {
            _animator.SetTrigger(_hashParmAttack2);
            //_animator.SetBool(_hashParmIsBusy, true);
        }
        public void PlayAIAnimDead()
        {
            _animator.SetTrigger(_hashParmDead);
            _animator.SetBool(_hashParmIsDead,true);
        }
        public void PlayAIAnimHit()
        {
            _animator.SetTrigger(_hashParmHit);
            //_animator.SetBool(_hashParmIsBusy, true);
        }

        void GetHashNames()
        {
            _hashParmMove = Animator.StringToHash("Move");
            _hashParmAttack1 = Animator.StringToHash("Attack1");
            _hashParmAttack2 = Animator.StringToHash("Attack2");
            _hashParmIsDead = Animator.StringToHash("IsDead");
            _hashParmDead = Animator.StringToHash("Dead");
            _hashParmHit = Animator.StringToHash("Hit");
            _hashParmIsBusy = Animator.StringToHash("IsBusy");
        }

        public void AnimEventAttack()
        {
            _aiController.AnimEventAttackCallback();
        }

        public void AnimatorBehaviourCallbackNotBusy()
        {
            _aiController.NotBusy();
        }
        //public 
    }
}