using System;
using KBCore.Refs;
using Sirenix.OdinInspector;
using FMOD;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [FoldoutGroup("References"), SerializeField, Anywhere]
    private DialogueHolderSO dialogue;

    public event Action<SSentence> OnSentenceChanged;
    public event Action OnDialogueEnded;

    private int currentIndex;

    public void StartDialogue(DialogueHolderSO newDialogue)
    {
        dialogue = newDialogue;
        currentIndex = 0;
        AdvanceDialogue();
            
    }

    public void AdvanceDialogue()
    {
        if (dialogue == null || dialogue.Sentences.Length == 0 || currentIndex >= dialogue.Sentences.Length)
        {
            EndDialogue();
            return;
        }

        OnSentenceChanged?.Invoke(dialogue.Sentences[currentIndex]);
        currentIndex++;
    }

    private void EndDialogue()
    {
        OnDialogueEnded?.Invoke();
        dialogue = null;
    }

}
