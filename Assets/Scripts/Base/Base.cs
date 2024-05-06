using System;
using Base.BuildStrategy;
using Base.Drone;
using Base.Stand;
using UnityEngine;

namespace Base
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private DroneDispatcher _droneDispatcher;
        [SerializeField] private StandHandler _standHandler;
        [SerializeField] private BaseRenderer _baseRenderer;
        [SerializeField] private BaseFlagHandler _baseFlagHandler;
        
        private BuildStrategy.BuildStrategy _buildStrategy;
        
        public ScoreCounter ScoreCounter => _scoreCounter;
        public BaseFlagHandler BaseFlagHandler => _baseFlagHandler;

        public event Action<Drone.Drone, Base> NotifyBaseSpawnerToBuildNewBase;

        private void OnEnable()
        {
            _standHandler.FreePlacesEnded += OnFreePlacesEnded;
            _baseFlagHandler.FlagInstalled += OnFlagInstalled;
            _droneDispatcher.NotifyBaseToBuildNewBase += InvokeNotifyBaseSpawnerToBuildNewBase;
            _droneDispatcher.DroneRemoved += OnDroneRemoved;
        }

        private void OnDisable()
        {
            _standHandler.FreePlacesEnded -= OnFreePlacesEnded;
            _baseFlagHandler.FlagInstalled -= OnFlagInstalled;
            _droneDispatcher.DroneRemoved -= OnDroneRemoved;
        }

        public void InitAsRoot(Color color, Vector3 position, DroneSpawner droneSpawner, int droneCount)
        {
            transform.position = position;
            _baseRenderer.SetColor(color);
            _droneDispatcher.Init(color, droneSpawner, droneCount);
            
        }
        
        public void InitAsNew(Color color, Vector3 position, DroneSpawner droneSpawner, int droneCount, Drone.Drone drone)
        {
            transform.position = position;
            _baseRenderer.SetColor(color);
            _droneDispatcher.Init(color, droneSpawner, droneCount);
            drone.ChangeBase(color, _standHandler.GetStand(), transform);
            _droneDispatcher.AddDrone(drone);
        }

        public void StartWork()
        {
            _buildStrategy = new BuildDroneStrategy(_scoreCounter, _droneDispatcher);
            _buildStrategy.Enter();
        }

        private void OnFreePlacesEnded()
        {
            if (_buildStrategy != null && _buildStrategy.GetType() == typeof(BuildDroneStrategy))
            {
                _buildStrategy.Exit();
            }
        }

        private void OnFlagInstalled(Transform position)
        {
            _buildStrategy.Exit();
            _buildStrategy = new BuildBaseStrategy(_scoreCounter, _droneDispatcher, position);
            _buildStrategy.Enter();
        }

        private void InvokeNotifyBaseSpawnerToBuildNewBase(Drone.Drone drone)
        {
            NotifyBaseSpawnerToBuildNewBase?.Invoke(drone, this);
            _droneDispatcher.NotifyBaseToBuildNewBase -= InvokeNotifyBaseSpawnerToBuildNewBase;
        }

        private void OnDroneRemoved()
        {
            _buildStrategy.Exit();
            _buildStrategy = new BuildDroneStrategy(_scoreCounter, _droneDispatcher);
            _buildStrategy.Enter();
            _baseFlagHandler.TakeFlag();
        }
    }
}
