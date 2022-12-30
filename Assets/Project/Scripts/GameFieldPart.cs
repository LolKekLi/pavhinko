using System;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(Collider))]
    public class GameFieldPart : MonoBehaviour, ICostMultiplier, IBuilding
    {
        [SerializeField]
        private float _costMultiplier = 2;

        [SerializeField]
        private bool _canDestroy = true;

        [SerializeField]
        private BuildingBase _buildingBase = null;

        private Transform _transform;

        public bool CanDestroy
        {
            get => _canDestroy;
        }
        
        public float CostMultiplier
        {
            get => _costMultiplier;
        }

        public Transform Transform
        {
            get => _transform;
        }

        private void Start()
        {
            _transform = transform;
        }

        public void Destroy()
        {
           _buildingBase.Destroy();
        }
    }
}