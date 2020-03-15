using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaviEnt;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField]
    string _questKey = string.Empty;

    private void Start()
    {
        OnQuestTrigger();
    }

    void OnQuestTrigger()
    {
        GameEventManager.Instance.OnQuestTriggerCallback(_questKey);
    }

}
