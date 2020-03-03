using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using NaviEnt.Data;
using NaviEnt.Game;
using System;

namespace NaviEnt.UI
{
    public class QuestMenu : Menu<QuestMenu>
    {
        Quest _quest;
        QuestManager _questManager = null;

        [SerializeField]
        TextMeshProUGUI _titleText = null;
        [SerializeField]
        TextMeshProUGUI _descriptionText = null;
        [SerializeField]
        TextMeshProUGUI _experienceText = null;
        [SerializeField]
        TextMeshProUGUI _goldText = null;


        // Start is called before the first frame update
        void Start()
        {
            _questManager = QuestManager.Instance;
            _quest = new Quest();
            
        }

        public override void MenuOpen()
        {   
            _titleText.text = _quest.title;
            _descriptionText.text = _quest.description;
            _experienceText.text = _quest.experienceReward.ToString();
            _goldText.text = _quest.coinReward.ToString();

            MenuPanel.SetActive(true);
        }

        public void AcceptQuestButton()
        {
            GameManager.Instance.AddNewQuest(_quest);
            MenuPanel.SetActive(false);
        }

       public void RefuseQuestButton()
        {
            MenuPanel.SetActive(false);
        }

    }
}