using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project
{
    public class UIBuildGroup : MonoBehaviour
    {
        [SerializeField]
        private Button _hideButton = null;
        
        [SerializeField]
        private UIBuildingItem[] _uiBuildingItems = null;

        [SerializeField]
        private RectTransform _buttonsContaiter = null;

        [Inject]
        private PoolManager _poolManager = null;

        private BuildingCreator _buildingCreator = null;

        private void Start()
        {
            _buildingCreator = new BuildingCreator(_poolManager);

            PrepareButtons();
            Hide();
        }
        
        public void Show(Camera gameCamera, BuildPoint buildPoint)
        {
            var showPositionUI = gameCamera.WorldToScreenPoint(buildPoint.transform.position);
            _buttonsContaiter.position = showPositionUI;
            
            UpdateCreatePosition(buildPoint);

            ShowButtons();
        }
        
        public void Hide()
        {
            HideButtons();
        }
        
        private void PrepareButtons()
        {
            _uiBuildingItems.Do(bi => bi.Setup(_buildingCreator, Hide));
            
            _hideButton.onClick.AddListener(Hide);
        }
        
        private void UpdateCreatePosition(BuildPoint buildPoint)
        {
            _uiBuildingItems.Do(bi => bi.UpdateCurrentBuildingPoint(buildPoint));
        }

        private void ShowButtons()
        {
            _uiBuildingItems.Do(bi => bi.Show());
            _hideButton.gameObject.SetActive(true);
        }

        private void HideButtons()
        {
            _uiBuildingItems.Do(bi => bi.Hide());
            _hideButton.gameObject.SetActive(false);
        }
    }
}