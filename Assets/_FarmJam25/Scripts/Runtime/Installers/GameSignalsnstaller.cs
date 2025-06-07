using NJG.Runtime.Signals;
using Zenject;

namespace NJG.Runtime.Installers
{
    public class GameSignalsnstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<CabinHealthChangedSignal>();
            Container.DeclareSignal<CabinDiedSignal>();
        }

    }
}