using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;


public class FeedbackHandler : MonoBehaviour
{
    [SerializeField]
    MMFeedbacks _hitFeedback = null;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitFeedback()
    {
        if(_hitFeedback != null)
        {
            _hitFeedback.PlayFeedbacks();
        }
    }
}
