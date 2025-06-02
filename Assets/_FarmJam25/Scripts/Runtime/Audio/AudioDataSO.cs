using FMODUnity;
using UnityEngine;
using Sirenix.OdinInspector;

namespace NJG.Runtime.Audio
{
	[CreateAssetMenu(menuName = "NJG/Audio/AudioData", fileName = "AudioDataSO")]
	public class AudioDataSO : ScriptableObject
	{
		[field: FoldoutGroup("Music"), SerializeField]
		public EventReference Mus_IdleNight {get; private set;}

		[field: FoldoutGroup("Music"), SerializeField]
		public EventReference Mus_CombatDay {get; private set;}

		[field: FoldoutGroup("Music"), SerializeField]
		public EventReference Mus_MenuTheme {get; private set;}

	}
}