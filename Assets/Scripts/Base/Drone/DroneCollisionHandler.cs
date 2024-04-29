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
                cube.transform.SetParent(transform);
                
                CubeFounded?.Invoke();
            }
        }
    }
}