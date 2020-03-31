using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaviEnt.Data;

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
        public string _key;
        public int _amount;

        private void Awake()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            
        }
        private void Start()
        {
            InitSpawnItem();
        }

        public void InitSpawnItem()
        {
            Transform objectPool = PoolManager.Instance.ActiveObject;

            if (transform.childCount==1)
            { 
                GameObject obj = GameManager.Instance.PickupItemInstantiate(_key, _amount, transform);
                obj.transform.parent = objectPool;
            }
        }

    }
}