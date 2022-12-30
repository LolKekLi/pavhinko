using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project
{
    public class DeleteBuildingButton : Button
    {
        private IBuilding _building;

        public void RefreshBuilding(IBuilding building)
        {
            _building = building;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            
            _building.Destroy();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}