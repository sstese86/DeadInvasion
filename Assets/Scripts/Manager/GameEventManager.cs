using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using NaviEnt.Data;

namespace NaviEnt.Game
{
    public class GameEventManager : MonoBehaviour
    {
        public static GameEventManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }
        public static event Action<Entity> onSelectedEntiyChanged = delegate { };
        public static event Action<Vector3> onMissionObjectSelected = delegate { };
        public static event Action onPlayerDataChanged = delegate { };


        public void OnMissionObjectSelected(Vector3 position)
        {
            if (onMissionObjectSelected != null)
                onMissionObjectSelected.Invoke(position);
        }

        public void OnPlayerDataChanged()
        {
            if (onPlayerDataChanged != null)
                onPlayerDataChanged.Invoke();
        }
    }
}