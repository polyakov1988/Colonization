using System;
using UnityEngine;

namespace Base.Drone
{
    public class DroneCollisionHandler : MonoBehaviour
    {
        public event Action CubeFounded;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Cube cube))
            {
                cube.OffGravity();

                if (cube.transform.parent == null && cube.CanBeTaken)
                {
                    cube.transform.SetParent(transform);
                    cube.Take();
                }
                
                CubeFounded?.Invoke();
            }
        }
    }
}