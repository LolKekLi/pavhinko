using Project.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Project
{
    public class InputManager : MonoBehaviour
    {
        [Inject]
        private JoystickController _joystickController = null;
        [Inject]
        private InGameCamera _inGameCamera = null;

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