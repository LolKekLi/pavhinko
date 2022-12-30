using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Meta
{
    public class User : StorageObject<UserStorageData>, IUser, ILevelData, IDisposable, IInitializable
    {
        private readonly ReactiveProperty<int> _coins = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<int> _levelIndex = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<int> _maxBallCount = new ReactiveProperty<int>(0);

        private readonly UniRxSubscribersContainer _subscribersContainer = new UniRxSubscribersContainer();

        public IReadOnlyReactiveProperty<int> Coins
        {
            get => _coins;
        }

        public IReadOnlyReactiveProperty<int> MaxBallCount
        {
            get => _maxBallCount;
        }

        public IReadOnlyReactiveProperty<int> LevelIndexProperty
        {
            get => _levelIndex;
        }

        public int LevelIndex
        {
            get => _levelIndex.Value; 
            set => _levelIndex.Value = Mathf.Max(0, value);
        }

        bool IUser.CanUpgrade(CurrencyType type, int amount)
        {
            bool canPurchase = false;

            switch (type)
            {
                case CurrencyType.Coin:
                    canPurchase = amount <= _coins.Value;
                    break;

                default:
                    canPurchase = true;
                    break;
            }

            return canPurchase;
        }

        void IUser.SetCurrency(CurrencyType type, int amount)
        {
            if (type == CurrencyType.Coin)
            {
                _coins.Value += amount;
            }
        }

        void IUser.SetMaxBallCount(int ballCount)
        {
            ConcreteData.MaxBallCount = ballCount;

            Save();

            _maxBallCount.Value = ConcreteData.MaxBallCount;
        }

        void IUser.UpgradeMaxBallCount()
        {
            _maxBallCount.Value++;
        }

        void IDisposable.Dispose()
        {
            Save();
        }

        void IInitializable.Initialize()
        {
            Load();

            _coins.Value = ConcreteData.MoneyCount;
            _levelIndex.Value = ConcreteData.LevelIndex;
            _maxBallCount.Value = ConcreteData.MaxBallCount;
            
            _subscribersContainer.Subscribe(Coins, i =>
            {
                ConcreteData.MoneyCount = i;
                
                Save();
            });
            
            _subscribersContainer.Subscribe(LevelIndexProperty, i =>
            {
                ConcreteData.LevelIndex = i;
                
                Save();
            });
            
            _subscribersContainer.Subscribe(MaxBallCount, i =>
            {
                ConcreteData.MaxBallCount = i;
                
                Save();
            });
        }
    }
}