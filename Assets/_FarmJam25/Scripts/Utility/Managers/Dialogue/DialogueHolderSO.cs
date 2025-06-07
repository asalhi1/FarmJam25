using UnityEngine;

[CreateAssetMenu(fileName = "DialogueHolderSO", menuName = "Scriptable Objects/DialogueHolderSO")]
public class DialogueHolderSO : ScriptableObject
{
    [SerializeField] public SSentence[] Sentences;
}

[System.Serializable]
public struct SSentence
{
    [field: SerializeField] public SpeakerSO Speaker { get; private set; }
    public string speakerName;
    public string dialogueText;
    public DialogueHolderSO nextLine;
}

public enum ESpeaker { necromancer, forestfriend1, forestfriend2}
