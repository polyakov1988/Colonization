using Base.Drone.StateMachine;
using Base.Drone.StateMachine.State;
using UnityEngine;

namespace Base.Drone
{
    public class Drone : MonoBehaviour
    {
        [SerializeField] private DroneAnimator _droneAnimator;
        [SerializeField] private DroneCollisionHandler _collisionHandler;
        [SerializeField] private float _speed;
        
        private DroneStateMachine _stateMachine;
        
        public Vector3 StandPosition { get; private set; }
        public Vector3 TargetPosition { get; private set; }
        public bool HasCube { get; private set; }
        public bool IsBusy { get; private set; }
        
        public DroneAnimator Animator => _droneAnimator;
        public float Speed => _speed;
        
        public void SetFree()
        {
            IsBusy = false;
            HasCube = false;
        }

        private void OnEnable()
        {
            _collisionHandler.CubeFounded += OnCubeFounded;
        }

        private void OnDisable()
        {
            _collisionHandler.CubeFounded -= OnCubeFounded;
        }

        public void Init(Transform parent, Vector3 standPosition)
        {
            _stateMachine = new DroneStateMachine(this);
            transform.SetParent(parent);
            transform.position = standPosition;
            StandPosition = standPosition;
            _droneAnimator.Init();
        }

        public void StartMining(Vector3 cubePosition)
        {
            IsBusy = true;
            TargetPosition = cubePosition;
            _stateMachine.SwitchState<MiningState>();
        }

        private void OnCubeFounded()
        {
            HasCube = true;
        }
    }
}