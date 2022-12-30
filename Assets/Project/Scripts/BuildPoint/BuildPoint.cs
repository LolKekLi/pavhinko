using UnityEngine;
using Zenject;

namespace Project
{
    public class BuildPoint : MonoBehaviour, IClickable
    {
        [Inject]
        private UIBuildGroup _buildGroup = null;

        [Inject]
        private InGameCamera _inGameCamera = null;

        public void Click()
        {
            _buildGroup.Show(_inGameCamera.Camera, this);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}