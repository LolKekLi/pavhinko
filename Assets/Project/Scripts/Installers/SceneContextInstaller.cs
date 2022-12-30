using Project.UI;
using Project.UIDebug;
using UnityEngine;
using Zenject;

namespace Project
{
    public class SceneContextInstaller : MonoInstaller
    {
        [SerializeField]
        private BallSpawner _ballSpawner = null;

        [SerializeField]
        private InGameCamera _inGameCamera = null;

        [SerializeField]
        private UIBuildGroup _uiBuildGroup = null;

        [Inject]
        private UISystem _uiSystem = null;

#if FORCE_DEBUG
        [Inject]
        private CustomDebugMenu _customDebugMenu = null;
#endif

        public override void InstallBindings()
        {
            Container.ParentContainers[0].Bind<BallSpawner>().FromInstance(_ballSpawner).AsCached();

            ((IForceInject) _uiSystem).ForceInject(Container);
            
            Container.ParentContainers[0].Bind<InGameCamera>().FromInstance(_inGameCamera).AsCached();
            Container.ParentContainers[0].Bind<UIBuildGroup>().FromInstance(_uiBuildGroup).AsCached();
            
#if FORCE_DEBUG
            Container.Inject(_customDebugMenu);
#endif
        }
    }
}       