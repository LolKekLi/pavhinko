using Project;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : PooledBehaviour
    {
        private Rigidbody _rb = null;

        public override void Prepare(PooledObjectType pooledType)
        {
            base.Prepare(pooledType);

            _rb = GetComponent<Rigidbody>();
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
    }
}
