using NJG.Runtime.Audio;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;
namespace NJG.Runtime.Installers
{
    public class AudioInstaller : MonoInstaller
    {
        [SerializeField, Tooltip("Path to the games audio data in the resources folder.")]
        private AudioDataSO audioData;
        public override void InstallBindings()
        {
            if (audioData == null)
            {
                Debug.LogError("[Audio] Cannot install Audio Manager, the Audio Data is Null");
            }
            else
            {
                Container.BindInstance(audioData).AsSingle();
                Container.Bind<GameObject>().FromInstance(gameObject).WhenInjectedInto<AudioManager>();
                Container.BindInterfacesAndSelfTo<AudioManager>().AsSingle().NonLazy();
            }
        }
    }
}