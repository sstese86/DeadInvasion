using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using DG.Tweening;
using NaviEnt.Data;

namespace NaviEnt.Game
{
    public class PickupItem : Item<PickupItem>, IEntity
    {

        [SerializeField]
        PickupItemTrigger _pickupTrigger = null;

        [SerializeField]
        AudioClip _pickupSound = null;

        DOTweenAnimation _dotween = null;

        GameObject player = null;

        public string EntityName { get; set; }
        public string EntityInfo { get; set; }

        private void Awake()
        {
            _pickupTrigger.gameObject.SetActive(false);
        }

        public void InitializePickupItem()
        {
            _pickupTrigger.gameObject.SetActive(true);
            DoTweenRotateItem();
        }

        public void OnPickupTriggerEnter(Collider other)
        {
            player = other.gameObject;
            if (player.GetComponent<CharacterHandler>().ActorTeam == Team.Player)
            {
                PickUp();
            }            
        }

        void PickUp()
        {
            AudioManager.Instance.PlaySoundSFX(_pickupSound);
            ItemData item = GameManager.Instance.GetItemData(Key);
            if(item.isEquipable)
            {
                player.GetComponent<EquipmentHandler>().EquipItem(Key);
                GameManager.Instance.AddPlayerItemAmount(Key, Amount);
            }
            else
            {
                GameManager.Instance.AddPlayerItemAmount(Key, Amount);
            }
            DoTweenRotateItemStop();
            _pickupTrigger.gameObject.SetActive(false);
            
            gameObject.SetActive(false);
        }

        void DoTweenRotateItem()
        {
            _dotween = GetComponent<DOTweenAnimation>();
            if (_dotween != null)
            {
                float value = _dotween.duration;
                _dotween.duration = Random.Range(value / 1.5f, value);
                _dotween.CreateTween();
                _dotween.DOPlay();
            }
        }

        void DoTweenRotateItemStop()
        {
            if (_dotween != null)
            {
                _dotween.DOPause();
            }
        }

        public void UpdateEntityInfo()
        {
            // No Need to Update Entity Info for Pick up items. because it will not change information.
        }
    }
}