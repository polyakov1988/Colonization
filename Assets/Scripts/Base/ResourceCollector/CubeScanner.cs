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

        public event Action<List<Cube>> CubesFounded; 
        
        private void Awake()
        {
            _isActive = true;
            _scanTimeout = new WaitForSeconds(_timeout);

            StartCoroutine(ScanArea());
        }

        private IEnumerator ScanArea()
        {
            while (_isActive)
            {
                List<Cube> cubes = new List<Cube>();

                Collider[] results = new Collider[6];
                int numColliders = Physics.OverlapSphereNonAlloc(transform.position, _scanRadius, results, _cubeMask);

                for (int i = 0; i < numColliders; i++)
                {
                    Cube cube = results[i].GetComponent<Cube>();

                    if (cube.IsReserved == false)
                    {
                        cube.Reserve();
                        cubes.Add(cube);
                    }
                }
                
                if (cubes.Count != 0) 
                    CubesFounded?.Invoke(cubes);

                yield return _scanTimeout;
            }
        }
    }
}