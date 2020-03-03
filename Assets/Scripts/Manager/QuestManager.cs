using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt.Data;
using NaviEnt.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    Quest _quest;
    QuestDatabase _questDatabase = null;

    private void Awake()
    {
        if (Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _questDatabase = Resources.Load<QuestDatabase>("ScriptableObject/QuestDatabase");
    }


    void OnQuestTriggerCallback(string questKey)
    {
        if (_questDatabase.data.TryGetValue(questKey, out _quest))
        {
            _quest.Key = questKey;
            
        }
        else
        {
            Debug.Log("QuestMenu:: Error get QuestValue from QuestDatabase is faild.");
        }


    }


}
