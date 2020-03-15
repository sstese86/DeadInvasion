using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaviEnt.Level;

public class BattleUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void ButtonMoveToMainManu()
    {
        LevelManager.Instance.MoveToMainMenu();
    }
}
