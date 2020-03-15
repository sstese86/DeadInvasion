using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt;

public class HUDActionButtons : MonoBehaviour
{
    public void AttackButtonPressed()
    {
        GameEventManager.Instance.OnPlayerAttackButtonPressed();
    }

    public void JumpButtonPressed()
    {
        GameEventManager.Instance.OnPlayerJumpButtonPressed();
    }
        
}
