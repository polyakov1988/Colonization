using System;
using System.Collections;
using System.Collections.Generic;
using Base.Stand;
using UnityEngine;

namespace Base.Drone
{
    public class DroneDispatcher : MonoBehaviour
    {
        [SerializeField] private StandHandler _standHandler;
        
        private DroneSpawner _droneSpawner;
        private List<Drone> _drones;
        private Color _color;
        private WaitForSeconds _searchFreeDroneTimeout;
        private int _startDroneCount;
        private Transform _flagPosition;

        public event Action<Drone> NotifyBaseToBuildNewBase;
        public event Action DroneRemoved;

        public void Init(Color color, DroneSpawner droneSpawner, int startDroneCount)
        {
            _startDroneCount = startDroneCount;
            _searchFreeDroneTimeout = new WaitForSeconds(1);
            _droneSpawner = droneSpawner;
            _drones = new List<Drone>();
            _standHandler.Init();
            _color = color;
            
            for (int i = 0; i < _startDroneCount; i++)
            {
                CreateDrone();
            }
        }

        public int GetFreeDronesCount()
        {
            int count = 0;
            
            foreach (var drone in _drones)
            {
                if (drone.IsBusy == false)
                {
                    count++;
                }
            }

            return count;
        }

        public void DoTasks(List<Transform> cubePositions)
        {
            List<Drone> freeDrones = _drones.FindAll(drone => drone.IsBusy == false);

            if (cubePositions.Count > freeDrones.Count)
                throw new ArgumentOutOfRangeException(nameof(cubePositions));

            for (int i = 0; i < cubePositions.Count; i++)
            {
                freeDrones[i].StartMining(cubePositions[i]);
            }
        }

        public void CreateDrone()
        {
            Drone drone = _droneSpawner.GetDrone();
            drone.BaseChanged += RemoveDrone;
            
            drone.Init(transform, _standHandler.GetStand(), _color);
                
            _drones.Add(drone);
        }

        public void SendDroneToBuildNewBase(Transform flagPosition)
        {
            _flagPosition = flagPosition;
            StartCoroutine(GetFreeDrone());
        }

        public void AddDrone(Drone drone)
        {
            _drones.Add(drone);
        }

        private IEnumerator GetFreeDrone()
        {
            Drone freeDrone = null;
            
            while (freeDrone == null)
            {
                freeDrone = _drones.Find(drone => drone.IsBusy == false);
                
                if (freeDrone != null)
                {
                    freeDrone.ReadyToBuildNewBase += InvokeNotifyBaseToBuildNewBase;
                    freeDrone.BuildNewBase(_flagPosition);
                }

                yield return _searchFreeDroneTimeout;
            }
        }

        private void InvokeNotifyBaseToBuildNewBase(Drone drone)
        {
            NotifyBaseToBuildNewBase?.Invoke(drone);
            drone.ReadyToBuildNewBase -= InvokeNotifyBaseToBuildNewBase;
        }

        private void RemoveDrone(Drone drone)
        {
            drone.BaseChanged -= RemoveDrone;
            _drones.Remove(drone);
            DroneRemoved?.Invoke();
        }
    }
}
