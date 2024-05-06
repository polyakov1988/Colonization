using System;
using UnityEngine;

namespace Base
{
    public class CollectorCollisionHandler : MonoBehaviour
    {
        public event Action CubeReceived;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Cube cube))
            {
                CubeReceived?.Invoke();
                cube.OffGravity();
                cube.InvokeUsed();
            }
        }
    }
}