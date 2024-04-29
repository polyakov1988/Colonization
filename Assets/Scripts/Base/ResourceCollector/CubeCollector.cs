using System;
using System.Collections.Generic;
using System.Linq;
using Base.Drone;
using UnityEngine;

namespace Base.ResourceCollector
{
    public class CubeCollector : MonoBehaviour
    {
        [SerializeField] private CubeScanner _cubeScanner;
        [SerializeField] private DroneDispatcher _droneDispatcher;

        private void OnEnable()
        {
            _cubeScanner.CubesFounded += StartMining;
        }

        private void OnDisable()
        {
            _cubeScanner.CubesFounded -= StartMining;
        }

        private void StartMining(List<Cube> foundedCubes)
        {
            int freeDronesCount = _droneDispatcher.GetFreeDronesCount();
            
            if (freeDronesCount == 0)
                return;

            if (foundedCubes.Count > freeDronesCount)
                foundedCubes = foundedCubes.GetRange(0, freeDronesCount - 1);
            
            _droneDispatcher.DoTasks(foundedCubes.Select(cube => cube.transform.position).ToList());
        }
    }
}
