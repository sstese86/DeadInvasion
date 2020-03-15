using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using NaviEnt.UI;
using NaviEnt;

public class DialogueMenu : Menu<DialogueMenu>
{

    Queue<string> sentences;
    Dialogue _currentDialogue;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    
    void Start()
    {
        GameEventManager.onDialogueTriggerCallback += UpdateNewDialogue;
        sentences = new Queue<string>();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameEventManager.onDialogueTriggerCallback -= UpdateNewDialogue;
    }

    void UpdateNewDialogue(Dialogue dialogue)
    {
        _currentDialogue = dialogue;
        StartDialogue();
    }

    void StartDialogue()
    {
        MenuOpen();
        nameText.text = _currentDialogue.name;
        sentences.Clear();

        foreach (string sentence in _currentDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void Button_NextSentence()
    {
        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char latter in sentence.ToCharArray())
        {
            dialogueText.text += latter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        MenuClose();
    }



}
