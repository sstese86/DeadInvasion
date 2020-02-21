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

        public override void OnClicked()
        {

            base.OnClicked();
            MissionEnterMenu.Instance.UpdateInfo(_missionId);
            MissionEnterMenu.MenuOpen();

        }
    }
}