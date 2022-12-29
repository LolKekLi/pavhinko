using Project.UIDebug;
using Zenject;

namespace Project
{
    public class SceneContextInstaller : MonoInstaller
    {
#if FORCE_DEBUG
        [Inject]
        private CustomDebugMenu _customDebugMenu = null;
#endif

        public override void InstallBindings()
        {
#if FORCE_DEBUG
            Container.Inject(_customDebugMenu);
#endif
        }
    }
}