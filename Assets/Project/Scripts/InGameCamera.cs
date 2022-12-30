using UnityEngine;

namespace Project
{
    public class InGameCamera : MonoBehaviour
    {
        [field: SerializeField]
        public Camera Camera
        {
            get;
            private set;
        } = null;
    }
}