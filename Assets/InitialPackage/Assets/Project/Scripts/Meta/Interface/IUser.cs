using UniRx;

namespace Project.Meta
{
    public interface IUser
    {
        public IReadOnlyReactiveProperty<int> Coins
        {
            get;
        }
        
        public IReadOnlyReactiveProperty<int> MaxBallCount
        {
            get;
        }
        
        bool CanUpgrade(CurrencyType type, int amount);
        void SetCurrency(CurrencyType type, int amount);
        void UpgradeMaxBallCount();
        void SetMaxBallCount(int ballCount);
    }
}