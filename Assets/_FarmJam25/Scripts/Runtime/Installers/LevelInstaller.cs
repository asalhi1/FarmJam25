using UnityEngine;
using Zenject;
using NJG.Runtime.Placement;
using Sirenix.OdinInspector;

namespace NJG.Runtime.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [FoldoutGroup("Scene Referances"), SerializeField]
        private GridManager _gridManager;
        
        public override void InstallBindings()
        {
            Container.Bind<GridManager>()
                .FromInstance(_gridManager)
                .AsSingle();
        }
    }
}