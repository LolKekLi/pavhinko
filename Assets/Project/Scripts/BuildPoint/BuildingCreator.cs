using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class BuildingCreator
    {
        private PoolManager _poolManager = null;

        public List<BuildingBase> SpawnedBuilding
        {
            get;
            private set;
        } = new List<BuildingBase>();

        public BuildingCreator(PoolManager poolManager)
        {
            _poolManager = poolManager;
        }

        public BuildingBase GetBuilding(BuildType buildType)
        {
            BuildingBase currentBuilding = null;
            
            switch (buildType)
            {
                case BuildType.Multiplier: 
                    currentBuilding = _poolManager.Get<Multiplier>(_poolManager.PoolSettings.Multiplier, Vector3.zero, Quaternion.identity);
                    break;
                
                case BuildType.Mill:
                    currentBuilding = _poolManager.Get<Mill>(_poolManager.PoolSettings.Mill, Vector3.zero, Quaternion.identity);
                    break;
                
                case BuildType.Roof:
                    currentBuilding = _poolManager.Get<Roof>(_poolManager.PoolSettings.Roof, Vector3.zero, Quaternion.identity);
                    break;
            }
            
            SpawnedBuilding.Add(currentBuilding);
            
            return currentBuilding;
        }
    }
}