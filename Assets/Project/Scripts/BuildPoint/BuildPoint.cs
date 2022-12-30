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
            _buildGroup.ShowBuildingButtons(_inGameCamera.Camera, this);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}