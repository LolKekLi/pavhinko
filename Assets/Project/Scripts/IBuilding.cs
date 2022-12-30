namespace Project
{
    public interface IBuilding : ITransformable
    {
        public bool CanDestroy
        {
            get;
        }

        void Destroy();
    }
}