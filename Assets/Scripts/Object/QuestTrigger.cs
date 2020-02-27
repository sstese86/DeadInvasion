using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.Game;

using UnityEngine.UI;
using TMPro;
using NaviEnt.Data;

public enum QuestType
{
    MainQuest,
    SubQuest,
    DayilyQuest
}

public class QuestTrigger : MonoBehaviour
{
    [SerializeField]
    QuestDatabase _questDatabase = null;

    Quest _quest;

    public QuestType questType;
    public string questKey = string.Empty;    


    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI experienceText;
    public TextMeshProUGUI goldText;

    private void Start()
    {
        GetQuestFromDatabase();
    }

    void GetQuestFromDatabase()
    {
        if (questType == QuestType.MainQuest) _questDatabase.mainQuest.TryGetValue(questKey, out _quest);
        else if (questType == QuestType.SubQuest) _questDatabase.subQuest.TryGetValue(questKey, out _quest);
        else if (questType == QuestType.DayilyQuest) _questDatabase.dayilyQuest.TryGetValue(questKey, out _quest);
        _quest.Key = questKey;
        _quest.Type = questType;
    }

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = _quest.title;
        descriptionText.text = _quest.description;
        experienceText.text = _quest.experienceReward.ToString();
        goldText.text = _quest.coinReward.ToString();
    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        _quest.isActive = true;
    }


}
