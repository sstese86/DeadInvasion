using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace NaviEnt
{
    public class GameEventManager : MonoBehaviour
    {
        public static GameEventManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }

        #region SYSTEM RELATED
        public static event Action onSavePlayerData = delegate { };
        public void OnSavePlayerData() 
        { 
            if(onSavePlayerData != null)
                onSavePlayerData.Invoke();            
        }

        public static event Action onPlayerDataChanged = delegate { };
        public void OnPlayerDataChanged()
        {
            if (onPlayerDataChanged != null)
                onPlayerDataChanged.Invoke();
        }

        #endregion


        #region UI RELATED

        public static event Action<IEntity, Transform> onSelectedEntityChangedCallback = delegate { };
        public void OnSelectedEntityChangedCallback(IEntity entityInfo, Transform trans)
        {
            if (onSelectedEntityChangedCallback != null)
                onSelectedEntityChangedCallback.Invoke(entityInfo, trans);
        }

        public static event Action<int> onEnemyRemainsUpdateCallback = delegate { };
        public void OnEenemyRemainsUpdateCallback (int enemyRemains)
        {
            if (onEnemyRemainsUpdateCallback != null)
                onEnemyRemainsUpdateCallback.Invoke(enemyRemains);
        }

        public static event Action onUIStateUpdate = delegate { };
        public void OnUIStateUpdate()
        {
            if (onUIStateUpdate != null)
                onUIStateUpdate.Invoke();
        }

        public static event Action<Vector3> onMissionObjectSelectedCallback = delegate { };
        public void OnMissionObjectSelected(Vector3 position)
        {
            if (onMissionObjectSelectedCallback != null)
                onMissionObjectSelectedCallback.Invoke(position);
        }
        
        public static event Action<string> onQuestTriggerCallback = delegate { };
        public void OnQuestTriggerCallback(string questKey)
        {
            if (onQuestTriggerCallback != null)
                onQuestTriggerCallback.Invoke(questKey);
        }

        public static event Action<Dialogue> onDialogueTriggerCallback = delegate { };
        public void OnDialogueTriggerCallback(Dialogue newDialogue)
        {
            if(onDialogueTriggerCallback != null)
                onDialogueTriggerCallback.Invoke(newDialogue);
        }

        #endregion


        #region Controller RELATED
        public static event Action onPlayerJumpButtonPressed = delegate { };
        public void OnPlayerJumpButtonPressed()
        {
            if (onPlayerJumpButtonPressed != null)
                onPlayerJumpButtonPressed.Invoke();
        }

        public static event Action onPlayerAttackButtonPressed = delegate { };
        public void OnPlayerAttackButtonPressed()
        {
            if(onPlayerAttackButtonPressed != null)
                onPlayerAttackButtonPressed.Invoke();
        }

        public static event Action onPlayerSkill1ButtonPressed = delegate { };
        public static event Action onPlayerSkill2ButtonPressed = delegate { };
        public static event Action onPlayerSkill3ButtonPressed = delegate { };
        public static event Action onPlayerSkill4ButtonPressed = delegate { };



        #endregion

        #region GamePlay
        public static event Action onEnemyKillCallback = delegate { };
        public void OnEnemyKillCallback()
        {
            if (onEnemyKillCallback != null)
                onEnemyKillCallback.Invoke();
        }

        public static event Action onItemCollectCallback = delegate { };
        public void OnItemCollectCallback()
        {
            if (onItemCollectCallback != null)
                onItemCollectCallback.Invoke();
        }

        public static event Action onPlayerDeadCallback = delegate { };
        public void OnPlayerDead()
        {
            if (onPlayerDeadCallback != null)
                onPlayerDeadCallback.Invoke();
        }

        public static event Action<Transform> onPlayerTargetChanged = delegate { };
        public void OnPlayerTargetChanged(Transform trans)
        {
            if (onPlayerTargetChanged != null)
                onPlayerTargetChanged.Invoke(trans);
        }

        #endregion







    }
}