using Project.Meta;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.UI
{
    public class GameWindow : Window
    {
        private readonly UniRxSubscribersContainer SubscribersContainer = new UniRxSubscribersContainer();
        
        [SerializeField, Space]
        private TextMeshProUGUI _ballCounter = null;

        [SerializeField]
        private Button _takeBallsButton = null;

        [SerializeField]
        private TextMeshProUGUI _coinCounter = null;

        [SerializeField]
        private Button _byBallButton = null;

        [SerializeField]
        private int _ballCost = 0;
        
        [InjectOptional]
        private BallSpawner _ballSpawner;

        private IUser _iUser;

        public override bool IsPopup
        {
            get => false;
        }

        [Inject]
        private void Construct(IUser iUser)
        {
            _iUser = iUser;
        }

        protected override void Start()
        {
            base.Start();
            
            _takeBallsButton.onClick.AddListener(OnTakeBallsButtonClick);
            _byBallButton.onClick.AddListener(OnByBallButtonClick);
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            if (_ballSpawner != null)
            {
                RefreshBallCounter();
                
                SubscribersContainer.Subscribe(_ballSpawner.MaxBallCount, i =>
                {
                    RefreshBallCounter();
                });

                SubscribersContainer.Subscribe(_ballSpawner.CurrentBallCount, i =>
                {
                    RefreshBallCounter();
                });
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            SubscribersContainer.FreeAllSubscribers();
        }

        protected override void Refresh()
        {
            base.Refresh();

            _coinCounter.text = $"{_iUser.Coins.Value}";
        }

        private void OnTakeBallsButtonClick()
        {
            _ballSpawner.ReturnBallFromContainer();
        }
        
        private void OnByBallButtonClick()
        {
            if (_iUser.CanUpgrade(CurrencyType.Coin, _ballCost))
            {
                _iUser.SetCurrency(CurrencyType.Coin, -_ballCost);
                _ballSpawner.UpgradeBallCount();
            }
        }
        
        private void RefreshBallCounter()
        {
            _ballCounter.text = $"{_ballSpawner.MaxBallCount.Value}/{_ballSpawner.CurrentBallCount.Value}";
        }
    }
}