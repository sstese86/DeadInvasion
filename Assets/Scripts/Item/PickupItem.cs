using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

namespace NaviEnt.Game
{
    public class PickupItem : Item<PickupItem>, IEntity
    {
        
        public string EntityName { get; set; }
        public string EntityInfo { get; set; }

        [SerializeField]
        MMFeedbacks _mmfeedback = null;
        
        
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            
            EntityName = ItemData.name;
            UpdateEntityInfo();
            OnStartCallback();
            _mmfeedback.Initialization();
        }
        
        public void UpdateEntityInfo()
        {
            EntityInfo = ItemData.description;
            GameEventManager.Instance.OnSelectedEntityChangedCallback(GetComponent<IEntity>());
        }

        public virtual void OnStartCallback()
        {

        }
        public void PickUp()
        {
            Debug.Log("PickedUp");
            _mmfeedback.PlayFeedbacks();
        }
        private void OnTriggerEnter(Collider other)
        {
            PickUp();
        }
    }
}