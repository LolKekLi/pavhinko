using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project
{
    public class UIBuildingItem : Button
    {
        private BuildingCreator _buildingCreator = null;
        
        private BuildPoint _currentBuildPoint = null;
        private Action _onClickCallback = null;

        [SerializeField]
        private BuildType _buildType = default;

        public void Setup(BuildingCreator buildingCreator, Action onClickCallback)
        {
            _buildingCreator = buildingCreator;
            _onClickCallback = onClickCallback;
        }

        public void UpdateCurrentBuildingPoint(BuildPoint buildPoint)
        {
            _currentBuildPoint = buildPoint;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            //NOTE: if enough money
            
            var buildingBase = _buildingCreator.GetBuilding(_buildType);

            buildingBase.transform.position = _currentBuildPoint.transform.position;
            buildingBase.SetupBuildPoint(_currentBuildPoint);

            _currentBuildPoint.Hide();
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