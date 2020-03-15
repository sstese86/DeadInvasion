using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.UI;
using NaviEnt.Game;

namespace NaviEnt
{
    [SelectionBase]
    public class MissionTrigger : ClickableObject
    {
        [SerializeField]
        string _key = string.Empty;
        protected override void OnMouseUpAsButton()
        {
            if(!GameManager.Instance.IsAnyMenuOpened)
            { 
                MissionEnterMenu.Instance.UpdateInfo(_key);
                GameEventManager.Instance.OnMissionObjectSelected(transform.position);
                MissionEnterMenu.Instance.MenuOpen();
            }

        }
    }
}