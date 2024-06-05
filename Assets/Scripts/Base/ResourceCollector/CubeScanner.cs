using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.ResourceCollector
{
    public class CubeScanner : MonoBehaviour
    {
        [SerializeField] private int _timeout;
        [SerializeField] private float _scanRadius;
        [SerializeField] private LayerMask _cubeMask;
        
        private WaitForSeconds _scanTimeout;
        private bool _isActive;
        private CubeHandler _cubeHandler;

        public event Action<List<Cube>> CubesFounded; 
        
        private void Awake()
        {
            _isActive = true;
            _scanTimeout = new WaitForSeconds(_timeout);
        }

        public void Init(CubeHandler cubeHandler)
        {
            _cubeHandler = cubeHandler;
            StartCoroutine(ScanArea());
        }

        private IEnumerator ScanArea()
        {
            while (_isActive)
            {
                yield return _scanTimeout;
                
                List<Cube> cubes = new List<Cube>();

                Collider[] results = new Collider[6];
                int numColliders = Physics.OverlapSphereNonAlloc(transform.position, _scanRadius, results, _cubeMask, QueryTriggerInteraction.Collide);

                for (int i = 0; i < numColliders; i++)
                {
                    Cube cube = results[i].GetComponent<Cube>();

                    if (_cubeHandler.CubeCanByReserved(cube))
                    {
                        _cubeHandler.ReserveCube(cube);
                        cubes.Add(cube);
                    }
                }
                
                if (cubes.Count != 0) 
                    CubesFounded?.Invoke(cubes);
            }
        }
    }
}