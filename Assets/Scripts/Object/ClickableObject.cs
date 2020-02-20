using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class ClickableObject : MonoBehaviour
{

    public MMFeedbacks _mmFeedback;
    
    private void OnMouseDown()
    {
        Debug.Log("I'm Clicked!!");
        _mmFeedback.PlayFeedbacks();
    }
}
