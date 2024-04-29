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
        private Collider[] _colliders;

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
                
                _colliders = Physics.OverlapSphere(transform.position, _scanRadius, _cubeMask);
                
                foreach (var coll in _colliders)
                {
                    Cube cube = coll.GetComponent<Cube>();

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