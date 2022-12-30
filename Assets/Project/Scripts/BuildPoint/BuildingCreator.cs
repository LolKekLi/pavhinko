namespace Project
{
    public class BuildingCreator
    {
        private PoolManager _poolManager = null;

        public BuildingCreator(PoolManager poolManager)
        {
            _poolManager = poolManager;
        }

        public BuildingBase GetBuilding(BuildType buildType)
        {
            //TODO: current buildings
            
            switch (buildType)
            {
                case BuildType.Multiplier: 
                    return null;
                    break;
                
                case BuildType.Mill:
                    return null;
                    break;
                
                case BuildType.Roof:
                    return null;
                    break;
            }
            
            return null;
        }
    }
}