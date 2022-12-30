using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(Collider))]
    public class GameFieldPart : MonoBehaviour, ICostMultiplier
    {
        [SerializeField]
        private float _costMultiplier = 2;
        
        public float CostMultiplier
        {
            get => _costMultiplier;
        }
    }
}