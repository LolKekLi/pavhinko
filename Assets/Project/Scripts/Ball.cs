using System;
using Project.Settings;
using UnityEngine;
using Zenject;

namespace Project
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : PooledBehaviour
    {
        private readonly int BaseColor = Shader.PropertyToID("_BaseColor");

        [SerializeField]
        private MeshRenderer _meshRenderer = null;

        private Rigidbody _rb = null;
        private BallSettings _ballSettings;
        private MaterialPropertyBlock _materialPropertyBlock;
        private PoolManager _poolManager;

        public float CurrentCost
        {
            get;
            private set;
        }

        [Inject]
        private void Construct(BallSettings ballSettings, PoolManager poolManager)
        {
            _poolManager = poolManager;
            _ballSettings = ballSettings;
        }

        public override void Prepare(PooledObjectType pooledType)
        {
            base.Prepare(pooledType);

            _rb = GetComponent<Rigidbody>();

            CurrentCost = _ballSettings.StartBallCost;

            _materialPropertyBlock = new MaterialPropertyBlock();

            ResetColor();
        }

        protected override void BeforeReturnToPool()
        {
            base.BeforeReturnToPool();

            _rb.ResetForce();

            CurrentCost = _ballSettings.StartBallCost;
            
            ResetColor();
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

                var costMultiplierEffect = _poolManager.Get<CostMultiplierEffect>(_poolManager.PoolSettings.CostMultiplierEffect,
                    collision.contacts.RandomElement().point, Quaternion.identity);
                
                costMultiplierEffect.Setup(multiplier.CostMultiplier);
                
                ChangeColor();
            }
        }
        
        private void ResetColor()
        {
            _meshRenderer.GetPropertyBlock(_materialPropertyBlock);
            _materialPropertyBlock.SetColor(BaseColor, _ballSettings.BallGradient.Evaluate(0));
            _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
        }

        private void ChangeColor()
        {
            _meshRenderer.GetPropertyBlock(_materialPropertyBlock);
            _materialPropertyBlock.SetColor(BaseColor, _ballSettings.BallGradient.Evaluate(CurrentCost / 100));
            _meshRenderer.SetPropertyBlock(_materialPropertyBlock);
        }

#if UNITY_EDITOR
        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CurrentCost++;
                ChangeColor();
            }
        }
#endif
    }
}