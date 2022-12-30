namespace Project
{
    public abstract class BuildingBase : PooledBehaviour
    {
        private BuildPoint _buildPoint;
        
        public void Destroy()
        {
            _buildPoint.Show();
            _buildPoint = null;
            Free();
        }
        
        public void SetupBuildPoint(BuildPoint buildPoint)
        {
            _buildPoint = buildPoint;
        }
    }
}