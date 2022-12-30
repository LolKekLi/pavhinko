using Project.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Project
{
    public class InputManager : MonoBehaviour
    {
        private JoystickController _joystickController = null;
        private InGameCamera _inGameCamera = null;
        private UIBuildGroup _uiBuildGroup = null;
        
        [Inject]
        private void Construct(JoystickController joystickController, InGameCamera inGameCamera,
            UIBuildGroup uiBuildGroup)
        {
            _uiBuildGroup = uiBuildGroup;
            _joystickController = joystickController;
            _inGameCamera = inGameCamera;
        }

        private void OnEnable()
        {
            _joystickController.Clicked += JoystickController_Clicked;
        }

        private void OnDisable()
        {
            _joystickController.Clicked -= JoystickController_Clicked;
        }

        private void JoystickController_Clicked(PointerEventData pointerEventData)
        {
            Ray ray = WrapPointer(pointerEventData.position);

            RayCastClickable(ray);
            RayCastBuilding(ray);
        }

        private void RayCastBuilding(Ray ray)
        {
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.TryGetComponent(out IBuilding building))
                {
                    if (building.CanDestroy)
                    {
                        _uiBuildGroup.ShowDestroyButton(building, _inGameCamera);
                    }
                }
            }
        }

        private static void RayCastClickable(Ray ray)
        {
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.TryGetComponent(out IClickable clickable))
                {
                    clickable.Click();
                }
            }
        }

        private Ray WrapPointer(Vector2 screenPos)
        {
            return _inGameCamera.Camera.ScreenPointToRay(screenPos);
        }
    }
}