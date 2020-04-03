using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.Game;
using System;
using TMPro;

namespace NaviEnt.UI
{

    public class HUDQuestReminder : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _valueText = null;
        
        BattleManager _battleManager = null;

        // Start is called before the first frame update
        void Start()
        {
            _battleManager = BattleManager.Instance;

            UpdateEnemyRemains();
        }

        private void OnEnable()
        {
            GameEventManager.onEnemyRemainsUpdateCallback += UpdateEnemyRemains;
        }

        private void OnDisable()
        {
            GameEventManager.onEnemyRemainsUpdateCallback -= UpdateEnemyRemains;
        }


        void UpdateEnemyRemains()
        {
            int value = _battleManager.EnemyRemains;
            _valueText.text = value.ToString();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}