using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project
{
    public class UIBuildingItem : Button
    {
        private BuildingCreator _buildingCreator = null;
        
        private Vector3 _createPosition = Vector3.zero;
        private Action _onClickCallback = null;

        [SerializeField]
        private BuildType _buildType = default;

        public void Setup(BuildingCreator buildingCreator, Action onClickCallback)
        {
            _buildingCreator = buildingCreator;
            _onClickCallback = onClickCallback;
        }

        public void UpdateCreatePosition(Vector3 createPosition)
        {
            _createPosition = createPosition;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            //NOTE: if enough money
            
            var buildingBase = _buildingCreator.GetBuilding(_buildType);

            buildingBase.transform.position = _createPosition;

            _onClickCallback.Invoke();
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