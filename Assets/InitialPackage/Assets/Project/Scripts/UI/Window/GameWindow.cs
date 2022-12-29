using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class GameWindow : Window
    {
        [SerializeField, Space]
        private TextMeshProUGUI _ballCounter = null;
        [SerializeField]
        private Button _takeBallsButton = null;
        
        public override bool IsPopup
        {
            get => false;
        }
    }
}