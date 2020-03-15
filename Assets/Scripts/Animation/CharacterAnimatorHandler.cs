using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NaviEnt.Game
{
    public class CharacterAnimatorHandler : MonoBehaviour
    {
        PlayerController _playerController = null;
        AIController _aiController = null;
        Animator _animator = null;
        // Start is called before the first frame update
        int _hashParmMoveSpeed = 0;
        int _hashParmJump = 0;
        int _hashParmIsJumping = 0;

        int _hashParmAttack = 0;
        int _hashParmWeaponIndex = 0;
        int _hashParmAttackAnimIndex = 0;

        float _layerFadeValue = 0f;
        float _upperLayerMaskWeight = 0f;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerController = transform.parent.transform.parent.GetComponent<PlayerController>();
            _aiController = transform.parent.transform.parent.GetComponent<AIController>();
        }

        void Start()
        {
            if(_playerController != null)
                _playerController.SetCharacterAnimatorHandler(this);
            GetHashNames();

        }
        private void Update()
        {
            LowerLayerMaskWeightUpdate();
        }

        void GetHashNames()
        {
            _hashParmMoveSpeed = Animator.StringToHash("MoveSpeed");
            _hashParmJump = Animator.StringToHash("Jump");
            _hashParmIsJumping = Animator.StringToHash("IsJumping");

            _hashParmAttack = Animator.StringToHash("Attack");
            _hashParmWeaponIndex = Animator.StringToHash("WeaponID");
            _hashParmAttackAnimIndex = Animator.StringToHash("AttackAnimID");
        }

        public void UpdateAnimParmMoveSpeed(float normalizedMagnitude)
        {
            _animator.SetFloat(_hashParmMoveSpeed, normalizedMagnitude);
            _upperLayerMaskWeight += normalizedMagnitude * 0.05f;
        }

        void LowerLayerMaskWeightUpdate()
        {
            _upperLayerMaskWeight -= 0.025f;
            _upperLayerMaskWeight = Mathf.Clamp01(_upperLayerMaskWeight);
            _animator.SetLayerWeight(1, _upperLayerMaskWeight);
        }


        public bool Jump()
        {
            if (_animator.GetBool(_hashParmIsJumping)) return false;

            _animator.SetTrigger(_hashParmJump);
            _animator.SetBool(_hashParmIsJumping, true);
            return true;
        }

        public bool Attack(int weaponIndex, float attackAnimIndex)
        {
            _animator.SetInteger(_hashParmWeaponIndex, weaponIndex);
            _animator.SetFloat(_hashParmAttackAnimIndex, attackAnimIndex);
            _animator.SetTrigger(_hashParmAttack);
            return true;
        }

        public void Dead()
        {
            _animator.SetTrigger("Dead");
        }

        public void CallNotBusy(float delay = 0f)
        {
            _playerController.NotBusy(delay);
        }

        public void CallNotCombatMode()
        {
            _playerController.NotCombatMode();
        }

        public void LayerWeightFadeRoutine(int layerIndex)
        {
            _layerFadeValue = 1f;
            StartCoroutine(LayerWeightFade(layerIndex));
        }


        IEnumerator LayerWeightFade( int layerIndex)
        {

            while(_layerFadeValue <= 0f)
            {
                _layerFadeValue -= 0.2f;
                _animator.SetLayerWeight(layerIndex, _layerFadeValue);
                yield return new WaitForSeconds(0.01f);

            }
            _animator.SetLayerWeight(layerIndex, 0f);
            _animator.SetBool("IsJumping", false);
            _playerController.NotBusy();
        }
    }
}
