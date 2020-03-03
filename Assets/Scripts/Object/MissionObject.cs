using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.UI;
using NaviEnt.Game;

namespace NaviEnt
{
    [SelectionBase]
    public class MissionObject : ClickableObject
    {
        [SerializeField]
        int _missionId = 0;

        protected override void OnMouseUpAsButton()
        {
            if(!GameManager.Instance.IsAnyMenuOpened)
            { 
                MissionEnterMenu.Instance.UpdateInfo(_missionId);
                GameEventManager.Instance.OnMissionObjectSelected(transform.position);
                MissionEnterMenu.Instance.MenuOpen();
            }

        }
    }
}