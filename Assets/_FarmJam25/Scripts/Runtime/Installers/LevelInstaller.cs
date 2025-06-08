using NJG.Runtime.Managers;
using UnityEngine;
using Zenject;
using Sirenix.OdinInspector;
using GridManager = NJG.Runtime.Placement.GridManager;

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
            
            Container.Bind<InventoryManager>()
                .AsSingle();

            Container.Bind<CabinManager>()
                .AsSingle();
            
        }
    }
}