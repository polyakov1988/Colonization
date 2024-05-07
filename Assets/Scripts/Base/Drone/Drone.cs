using System;
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
        [SerializeField] private Renderer _renderer;
        
        private DroneStateMachine _stateMachine;

        public event Action<Drone> ReadyToBuildNewBase;
        public event Action<Drone> BaseChanged;
        
        public Stand.Stand Stand { get; private set; }
        public Transform CubePosition { get; private set; }
        public bool HasCube { get; private set; }
        public bool IsBusy { get; private set; }
        
        public Transform FlagPosition { get; private set; }
        
        public DroneAnimator Animator => _droneAnimator;
        public float Speed => _speed;
        
        private void OnEnable()
        {
            _collisionHandler.CubeFounded += OnCubeFounded;
        }

        private void OnDisable()
        {
            _collisionHandler.CubeFounded -= OnCubeFounded;
        }
        
        public void SetFree()
        {
            HasCube = false;
            CubePosition = null;
            FlagPosition = null;
            IsBusy = false;
        }
        
        public void SetBusy()
        {
            IsBusy = true;
        }
        
        public void Init(Transform parent, Stand.Stand stand, Color color, CubeHandler cubeHandler)
        {
            _renderer.material.color = color;
            _stateMachine = new DroneStateMachine(this);
            transform.SetParent(parent);
            transform.position = stand.transform.position;
            Stand = stand;
            _droneAnimator.Init();
            _collisionHandler.Init(cubeHandler);
        }

        public void StartMining(Transform cubePosition)
        {
            CubePosition = cubePosition;
            _stateMachine.SwitchState<MiningState>();
        }
        
        public void BuildNewBase(Transform flagPosition)
        {
            IsBusy = true;
            FlagPosition = flagPosition;
            _stateMachine.SwitchState<BuildingBaseState>();
        }

        public void InvokeReadyToBuildNewBase()
        {
            ReadyToBuildNewBase?.Invoke(this);
        }

        public void ChangeBase(Color newColor, Stand.Stand newStand, Transform parent)
        {
            _renderer.material.color = newColor;
            transform.SetParent(parent);
            Stand.SetFree();
            Stand = newStand;
            BaseChanged?.Invoke(this);
            _stateMachine.SwitchState<ReturningToBaseState>();
        }

        private void OnCubeFounded()
        {
            HasCube = true;
        }
    }
}