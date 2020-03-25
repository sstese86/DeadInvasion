using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class TrapTrigger : MonoBehaviour
{
    [SerializeField]
    Team _team = Team.Prop;
    [SerializeField]
    int _damage = 1;
    [SerializeField]
    MMFeedbacks _triggerFeedback = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable target = other.GetComponent<IDamageable>();
        if (target == null) return;
        target?.TakeDamage(_team, _damage);

        _triggerFeedback?.PlayFeedbacks();
    }
}
