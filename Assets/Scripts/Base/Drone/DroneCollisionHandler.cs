using System;
using UnityEngine;

namespace Base.Drone
{
    public class DroneCollisionHandler : MonoBehaviour
    {
        private CubeHandler _cubeHandler;
        
        public event Action CubeFounded;

        public void Init(CubeHandler cubeHandler)
        {
            _cubeHandler = cubeHandler;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Cube cube))
            {
                if (_cubeHandler.CubeCanByTaken(cube))
                {
                    cube.transform.SetParent(transform);
                    cube.InvokeMoved();
                }
                
                CubeFounded?.Invoke();
            }
        }
    }
}