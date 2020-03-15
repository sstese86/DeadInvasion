using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace NaviEnt.Game
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }
        DialogueDatabase _dialogueDatabase;



        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;

            _dialogueDatabase = Resources.Load<DialogueDatabase>("ScriptableObject/DialogueDatabase");
        }

        public void StartDialogue(string key)
        {
            Dialogue newDialogue = new Dialogue();
            newDialogue = _dialogueDatabase.data[key];
            GameEventManager.Instance.OnDialogueTriggerCallback(newDialogue);
        }

    }
}