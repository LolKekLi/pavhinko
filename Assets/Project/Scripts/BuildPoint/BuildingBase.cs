using UnityEngine;

namespace Project
{
    public abstract class BuildingBase : PooledBehaviour, ICostMultiplier
    {
        public float CostMultiplier
        {
            get => 2f;
        }
    }
}