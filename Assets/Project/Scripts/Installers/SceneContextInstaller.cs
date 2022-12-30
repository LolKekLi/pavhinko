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
            
#if FORCE_DEBUG
            Container.Inject(_customDebugMenu);
#endif
        }
    }
}       