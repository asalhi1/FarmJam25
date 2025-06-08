using System.Collections.Generic;
using KBCore.Refs;
using MEC;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Tools = NJG.Utilities.Tools;

[RequireComponent(typeof(CanvasGroup))]
public class DialogueUI : MonoBehaviour
{
    [FoldoutGroup("References"), SerializeField, Self]
    private CanvasGroup canvasGroup;

    [FoldoutGroup("References"), SerializeField, Anywhere]
    private TextMeshProUGUI speakerText;

    [FoldoutGroup("References"), SerializeField, Anywhere]
    private TextMeshProUGUI messageText;

    [FoldoutGroup("References"), SerializeField, Anywhere]
    private DialogueManager manager;

    [FoldoutGroup("Settings"), SerializeField]
    private float typeSpeed = 0.05f;

    private CoroutineHandle typingHandle;

    private void OnEnable()
    {
        manager.OnSentenceChanged += AnimateText;
        manager.OnDialogueEnded += HideUI;
    }

    private void OnDisable()
    {
        manager.OnSentenceChanged -= AnimateText;
        manager.OnDialogueEnded -= HideUI;
    }

    public void ShowUI() => 
        Tools.ToggleVisibility(canvasGroup, true);

    public void HideUI() =>
        Tools.ToggleVisibility(canvasGroup, false);

    private void AnimateText(SSentence sentence)
    {
        ShowUI();
        speakerText.SetText(sentence.speakerName);
        typingHandle =
            Timing.RunCoroutine(TypeWriterEffect(sentence.dialogueText).CancelWith(gameObject));
    }

    private IEnumerator<float> TypeWriterEffect(string message)
    {
        messageText.text = string.Empty; 

        foreach (char c in message)
        {
            messageText.text += c;
            yield return
        Timing. WaitForSeconds(typeSpeed);
        }
    }
}
