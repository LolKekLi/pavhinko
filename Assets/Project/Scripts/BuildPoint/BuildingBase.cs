using UnityEngine;

namespace Project
{
    public abstract class BuildingBase : MonoBehaviour
    {
        [field: SerializeField]
        public BuildType BuildType
        {
            get;
            private set;
        }
    }
}