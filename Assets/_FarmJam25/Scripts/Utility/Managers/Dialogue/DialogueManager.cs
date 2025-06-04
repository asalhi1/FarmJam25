using FMOD;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Text speakerText;

    public DialogueHolderSO startingLine;
    public DialogueHolderSO currentLine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLine = startingLine;
        DisplayLine(currentLine);
    }

    public void DisplayLine(DialogueHolderSO line)
    {
        if (line == null)
            return;

        //speakerText.text = line.speakerName;
        //dialogueText.text = line.dialogueText;

  
    }
}
