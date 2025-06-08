using UnityEngine;

[CreateAssetMenu(fileName = "SO_Speaker", menuName = "Scriptable Objects/SpeakerSO")]
public class SpeakerSO : ScriptableObject
{
    [SerializeField] private string _speakerName;
    
    public string SpeakerName => _speakerName;
}
