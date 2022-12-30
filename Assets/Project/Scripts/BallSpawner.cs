using System.Collections.Generic;
using Project.Meta;
using Project.UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField]
        private float _force = 0f;

        [SerializeField]
        private Vector2 _spawnForceDirection = Vector2.zero;

        [SerializeField]
        private Transform _spawnPostion = null;

        [SerializeField]
        private BallContainer _ballContainer = null;
        
        private JoystickController _joystickController = null;
        private PoolManager _poolManager = null;
        
        private ReactiveProperty<int> _currentBallCount = new ReactiveProperty<int>(0);
        
        private List<Ball> _containerBalls = new List<Ball>();
        private IUser _iUser;

        public IReadOnlyReactiveProperty<int> CurrentBallCount
        {
            get => _currentBallCount;
        }
        
        [Inject]
        private void Construct(JoystickController joystickController, PoolManager poolManager, IUser iUser)
        {
            _iUser = iUser;
            _poolManager = poolManager;
            _joystickController = joystickController;
        }

        private void Start()
        {
            _currentBallCount.Value = _iUser.MaxBallCount.Value;
           
            _ballContainer.Setup(ball =>
            {
                if (!_containerBalls.Contains(ball))
                {
                    _containerBalls.Add(ball);
                }
               
            }, ball =>
            {
                if (_containerBalls.Contains(ball))
                {
                    _containerBalls.Remove(ball);
                }
            });
        }

        private void OnEnable()
        {
            _joystickController.Clicked += JoystickController_Clicked;
        }

        private void OnDisable()
        {
            _joystickController.Clicked -= JoystickController_Clicked;
        }

        public void ReturnBallFromContainer()
        {
            _containerBalls.Do(ball =>
            {
                _iUser.SetCurrency(CurrencyType.Coin, (int)ball.CurrentCost);
                
                ball.Free();
                
                _currentBallCount.Value++;
            });
            
            _containerBalls.Clear();
        }

        private void SpawnBall()
        {
            _currentBallCount.Value--;

            var ball = _poolManager.Get<Ball>(_poolManager.PoolSettings.Ball, _spawnPostion.position,
                Quaternion.identity);

            ball.AddForce(_spawnForceDirection * _force);
        }
        
        public void UpgradeBallCount()
        {
            _currentBallCount.Value++;
        }

        private void JoystickController_Clicked()
        {
            if (_currentBallCount.Value > 0)
            {
                SpawnBall();
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