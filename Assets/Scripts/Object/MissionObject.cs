using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.UI;

namespace NaviEnt
{
    public class MissionObject : ClickableObject
    {
        [SerializeField]
        int _missionId = 0;

        protected override void OnMouseUpAsButton()
        {
            MissionEnterMenu.Instance.UpdateInfo(_missionId);
            MissionEnterMenu.MenuOpen();

            GameEventManager.Instance.OnMissionObjectSelected(transform.position);
        }
    }
}