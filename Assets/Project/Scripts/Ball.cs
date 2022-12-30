using System;
using UnityEngine;
using Zenject;

namespace Project
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : PooledBehaviour
    {
        private Rigidbody _rb = null;
        private BallSettings _ballSettings;

        public float CurrentCost
        {
            get;
            private set;
        }


        [Inject]
        private void Construct(BallSettings ballSettings)
        {
            _ballSettings = ballSettings;
        }

        public override void Prepare(PooledObjectType pooledType)
        {
            base.Prepare(pooledType);

            _rb = GetComponent<Rigidbody>();

            CurrentCost = _ballSettings.StartBallCost;
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
