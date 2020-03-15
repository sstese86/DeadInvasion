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
        
        

        void Update()
        {

        }
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            
            EntityName = ItemData.name;
            EntityInfo = ItemData.description;

            UpdateEntityInfo();
            OnStartCallback();
            _mmfeedback.Initialization();
        }
        
        public void UpdateEntityInfo()
        {
            GameEventManager.Instance.OnSelectedEntityChangedCallback(GetComponent<IEntity>());
        }

        public virtual void OnStartCallback()
        {

        }

        public void PickUp()
        {
            GameManager.Instance.AddPlayerItemAmount(ItemData.name, Amount);
            _mmfeedback.PlayFeedbacks();
        }
        private void OnTriggerEnter(Collider other)
        {
            PickUp();
        }
    }
}