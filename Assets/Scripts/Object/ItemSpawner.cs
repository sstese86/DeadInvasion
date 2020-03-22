using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaviEnt.Data;

//TODO make this to PickUpItem. that dosen't need premade prefab.

namespace NaviEnt.Game
{ 

    public struct ItemSpawnData
    {
        public string _key;
        public int _amount;
    }

    [SelectionBase]
    public class ItemSpawner : MonoBehaviour
    {
        //[SerializeField]
        //List<ItemSpawnData> _spawnList = new List<ItemSpawnData>();
        [SerializeField]
        public string _key;
        [SerializeField]
        public int _amount;

        private void Awake()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        private void Start()
        {
            InitSpawnItem();  
        }

        void InitSpawnItem()
        {
            Transform battleGameObject = GameObject.Find("/[BattleGameActor]/Pickup").transform;

            if (transform.childCount==1)
            { 
                GameObject obj = GameManager.Instance.PickupItemInstantiate(_key, _amount, transform);
                obj.transform.parent = battleGameObject;
            }
        }

        public void PickUp()
        {
            //GameManager.Instance.AddPlayerItemAmount(ItemData.name, Amount);
        }
        private void OnTriggerEnter(Collider other)
        {
            PickUp();
        }
    }
}