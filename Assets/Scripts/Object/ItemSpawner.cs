using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaviEnt.Data;
using NaviEnt.Game;

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

    private void Start()
    {
        SpawnItem();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        SpawnItem();
    }

    void SpawnItem()
    {
        if(transform.childCount==1)
        { 
            GameManager.Instance.ItemInstantiate(_key, _amount, transform);
        }
    }
}
