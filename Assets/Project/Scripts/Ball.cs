using System;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : PooledBehaviour
    {
        [SerializeField, Space]
        private float _startCost = 1;
        
        private Rigidbody _rb = null;
        
        public float CurrentCost
        {
            get;
            private set;
        }

        public override void Prepare(PooledObjectType pooledType)
        {
            base.Prepare(pooledType);

            _rb = GetComponent<Rigidbody>();

            CurrentCost = _startCost;
        }

        protected override void BeforeReturnToPool()
        {
            base.BeforeReturnToPool();
            
            _rb.ResetForce();
        }

        public void AddForce(Vector3 force)
        {
            _rb.AddForce(force, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out ICostMultiplier multiplier))
            {
                CurrentCost *= multiplier.CostMultiplier;
            }
        }
    }
}
