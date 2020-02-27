using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    DialogueManager dialogueManager = null;

    private void Start()
    {
        dialogueManager = DialogueManager.Instance;
    }


    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}
