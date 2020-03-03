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
        
        public static event Action<IEntity> onSelectedEntityChangedCallback = delegate { };
        public static event Action onUIStateUpdate = delegate { };
        
        public static event Action onEnemyKillCallback = delegate { };
        public static event Action onItemCollectCallback = delegate { };

        public static event Action<Vector3> onMissionObjectSelectedCallback = delegate { };
        public static event Action onPlayerDataChangedCallback  = delegate { };
        
        public static event Action<string> onQuestTriggerCallback = delegate { };


        public void OnSelectedEntityChangedCallback(IEntity entityInfo)
        {
            if (onSelectedEntityChangedCallback != null)
                onSelectedEntityChangedCallback.Invoke(entityInfo);
        }

        public void OnUIStateUpdate()
        {
            if (onUIStateUpdate != null)
                onUIStateUpdate.Invoke();
        }

        public void OnEnemyKillCallback()
        {
            if (onEnemyKillCallback != null)
                onEnemyKillCallback.Invoke();
        }

        public void OnItemCollectCallback()
        {
            if (onItemCollectCallback != null)
                onItemCollectCallback.Invoke();
        }

        public void OnMissionObjectSelected(Vector3 position)
        {
            if (onMissionObjectSelectedCallback != null)
                onMissionObjectSelectedCallback.Invoke(position);
        }

        public void OnPlayerDataChanged()
        {
            if (onPlayerDataChangedCallback != null)
                onPlayerDataChangedCallback.Invoke();
        }

        public void OnQuestTriggerCallback(string questKey)
        {
            if (onQuestTriggerCallback != null)
                onQuestTriggerCallback.Invoke(questKey);
        }

    }
}