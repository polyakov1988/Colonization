using System;
using System.Collections.Generic;
using Base.Stand;
using UnityEngine;

namespace Base.Drone
{
    public class DroneDispatcher : MonoBehaviour
    {
        [SerializeField] private DroneSpawner _droneSpawner;
        [SerializeField] private StandHandler _standHandler;
        [SerializeField] private int _startDroneCount;
        
        private List<Drone> _drones;

        private void Awake()
        {
            _drones = new List<Drone>();
            _standHandler.Init();
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < _startDroneCount; i++)
            {
                Drone drone = _droneSpawner.GetDrone();

                Vector3 standPosition = _standHandler.GetStandPositionByIndex(i);
                
                drone.Init(transform, standPosition);
                
                _drones.Add(drone);
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

        public void DoTasks(List<Vector3> cubePositions)
        {
            List<Drone> freeDrones = _drones.FindAll(drone => drone.IsBusy == false);

            if (cubePositions.Count > freeDrones.Count)
                throw new ArgumentOutOfRangeException(nameof(cubePositions));

            for (int i = 0; i < cubePositions.Count; i++)
            {
                freeDrones[i].StartMining(cubePositions[i]);
            }
        }
    }
}
