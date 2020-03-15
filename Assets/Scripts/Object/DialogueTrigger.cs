using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaviEnt.Game;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    string _dialogueKey = string.Empty;
    
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(_dialogueKey);
    }
}
