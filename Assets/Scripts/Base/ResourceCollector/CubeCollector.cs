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
            {
                foreach (Cube foundedCube in foundedCubes)
                {
                    foundedCube.CancelReserve();
                    
                }
                
                return;
            }

            List<Cube> targetCubes = foundedCubes;
            
            if (foundedCubes.Count > freeDronesCount)
            {
                targetCubes = foundedCubes.GetRange(0, freeDronesCount - 1);

                for (int i = freeDronesCount; i < foundedCubes.Count; i++)
                {
                    foundedCubes[i].CancelReserve();
                }
            }
            
            _droneDispatcher.DoTasks(targetCubes.Select(cube => cube.transform).ToList());
        }
    }
}
