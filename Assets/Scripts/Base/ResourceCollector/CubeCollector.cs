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

        private CubeHandler _cubeHandler;
        
        private void OnEnable()
        {
            _cubeScanner.CubesFounded += StartMining;
        }

        private void OnDisable()
        {
            _cubeScanner.CubesFounded -= StartMining;
        }

        public void Init(CubeHandler cubeHandler)
        {
            _cubeHandler = cubeHandler;
            _cubeScanner.Init(_cubeHandler);
            
        }

        private void StartMining(List<Cube> foundedCubes)
        {
            int freeDronesCount = _droneDispatcher.GetFreeDronesCount();

            if (freeDronesCount == 0)
            {
                foreach (Cube foundedCube in foundedCubes)
                {
                    _cubeHandler.CancelReserveCube(foundedCube);
                }
                
                return;
            }

            List<Cube> targetCubes = foundedCubes;
            
            if (foundedCubes.Count > freeDronesCount)
            {
                targetCubes = foundedCubes.GetRange(0, freeDronesCount);

                for (int i = freeDronesCount; i < foundedCubes.Count; i++)
                {
                    _cubeHandler.CancelReserveCube(foundedCubes[i]);
                }
            }
            
            _droneDispatcher.DoTasks(targetCubes.Select(cube => cube.transform).ToList());
        }
    }
}
