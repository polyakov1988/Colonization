using UnityEngine;

namespace Base.Drone
{
    public class DroneSpawner : MonoBehaviour
    {
        [SerializeField] private Drone _dronePrefab;
        
        public Drone GetDrone()
        {
            return Instantiate(_dronePrefab);
        }
    }
}