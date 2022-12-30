using Project.Meta;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        private TextMeshProUGUI _ballCostText = null;

        [InjectOptional]
        private BallSpawner _ballSpawner;

        private IUser _iUser;
        private ILevelData _iLevelData;
        private BallSettings _ballSettings;

        public override bool IsPopup
        {
            get =>
                false;
        }

        private int CurrentBallCost
        {
            get =>
                _ballSettings.StartBallCost + (int)(_ballSettings.BallCostMultiplier *
                    LocalConfig.GetBallCostLevel(_iLevelData.LevelIndex));
        }

        [Inject]
        private void Construct(IUser iUser, ILevelData iLevelData, BallSettings ballSettings)
        {
            _ballSettings = ballSettings;
            _iLevelData = iLevelData;
            _iUser = iUser;
        }

        protected override void Start()
        {
            base.Start();

            _takeBallsButton.onClick.AddListener(OnTakeBallsButtonClick);
            _byBallButton.onClick.AddListener(OnByBallButtonClick);

            _ballCostText.text = $"${CurrentBallCost}";
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (_ballSpawner != null)
            {
                RefreshBallCounter();

                SubscribersContainer.Subscribe(_iUser.MaxBallCount, i => { RefreshBallCounter(); });

                SubscribersContainer.Subscribe(_ballSpawner.CurrentBallCount, i => { RefreshBallCounter(); });
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
            _ballCostText.text = $"{CurrentBallCost}";
        }

        private void OnTakeBallsButtonClick()
        {
            _ballSpawner.ReturnBallFromContainer();
        }

        private void OnByBallButtonClick()
        {
            var currentBallCost = CurrentBallCost;
            if (_iUser.CanUpgrade(CurrencyType.Coin, currentBallCost))
            {
                _iUser.SetCurrency(CurrencyType.Coin, -currentBallCost);

                LocalConfig.SetBallCostLevel(_iLevelData.LevelIndex,
                    LocalConfig.GetBallCostLevel(_iLevelData.LevelIndex) + 1);

                _iUser.UpgradeMaxBallCount();
                _ballSpawner.UpgradeBallCount();
            }
        }

        private void RefreshBallCounter()
        {
            _ballCounter.text = $"{_iUser.MaxBallCount.Value}/{_ballSpawner.CurrentBallCount.Value}";
        }
    }
}