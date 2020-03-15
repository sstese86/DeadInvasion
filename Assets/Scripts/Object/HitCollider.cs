using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    [SerializeField]
    Team _team = Team.Player;
    [SerializeField]
    int _damage = 0;

    [SerializeField]
    bool _debugMode = false;
    
    private void OnTriggerEnter(Collider other)
    {
        IDamageable target = other.GetComponent<IDamageable>();
        target?.TakeDamage(_team, _damage);
        GetComponent<Collider>().enabled = false;
        if(_debugMode)
            GetComponent<MeshRenderer>().enabled = false;
    }

    public void InitHitCollider(Team team, int damage)
    {
        this._team = team;
        this._damage = damage;
        GetComponent<Collider>().enabled = true;

        if (_debugMode)
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
        else
        { 
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(_debugMode)
        { 
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
        else
            GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
