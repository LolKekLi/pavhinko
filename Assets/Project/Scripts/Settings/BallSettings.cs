using UnityEngine;

namespace Project
{
    [CreateAssetMenu(menuName = "Scriptable/BallSettings", fileName = "BallSettings", order = 0)]
    public class BallSettings : ScriptableObject
    {
        [field: SerializeField]
        public int StartMaxBallCount
        {
            get;
            private set;
        }

        [field: SerializeField]
        public int StartBallCost
        {
            get;
            private set;
        }

        [field: SerializeField]
        public Gradient BallGradient
        {
            get;
            private set;
        }

        [field: SerializeField]
        public float BallCostMultiplier
        {
            get;
            private set;
        }
    }
}
