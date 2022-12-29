using Project.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField]
        private int _startBallCount = 0;
        
        [SerializeField]
        private float _force = 0f;

        [SerializeField]
        private Vector2 _spawnForceDirection = Vector2.zero;

        [SerializeField]
        private Transform _spawnPostion = null;
        
        private JoystickController _joystickController = null;
        private PoolManager _poolManager = null;
        
        private ReactiveProperty<int> _currentBallCount = new ReactiveProperty<int>(0);
        
        public IReadOnlyReactiveProperty<int> Coins
        {
            get => _currentBallCount;
        }

        [Inject]
        private void Construct(JoystickController joystickController, PoolManager poolManager)
        {
            _poolManager = poolManager;
            _joystickController = joystickController;
        }

        private void Start()
        {
            _currentBallCount.Value = _startBallCount;
        }

        private void OnEnable()
        {
            _joystickController.Clicked += JoystickController_Clicked;
        }

        private void OnDisable()
        {
            _joystickController.Clicked -= JoystickController_Clicked;
        }

        private void SpawnBall()
        {
            _currentBallCount.Value--;

            var ball = _poolManager.Get<Ball>(_poolManager.PoolSettings.Ball, _spawnPostion.position,
                Quaternion.identity);

            ball.AddForce(_spawnForceDirection * _force);
        }

        private void JoystickController_Clicked()
        {
            SpawnBall();
            if (_currentBallCount.Value >= 0)
            {
               
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            DrawSpawnForceDirection();
        }

        private void DrawSpawnForceDirection()
        {
            if (_spawnPostion != null)
            {
                Gizmos.color = Color.red;

                Gizmos.DrawRay(_spawnPostion.position, _spawnForceDirection.normalized * 100000);
            }
        }
#endif
    }
}