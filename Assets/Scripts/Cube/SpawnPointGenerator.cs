using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPointGenerator : MonoBehaviour
{
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minZ;
    [SerializeField] private float _maxZ;
    [SerializeField] private float _step;
    [SerializeField] private float _radiusDetection;
    [SerializeField] private LayerMask _baseMask;
        
    private List<Vector3> _spawnPoints;

    private void Awake()
    {
        _spawnPoints = new List<Vector3>();
            
        Generate();
    }

    private void Generate()
    {
        for (int x = (int) _minX; x < _maxX; x += (int) _step)
        {
            for (int z = (int) _minZ; z < _maxZ; z += (int) _step)
            {
                Vector3 point = new (x, 0, z);
                
                _spawnPoints.Add(point);
            }
        }
        
        removeBadPoints();
    }

    private void removeBadPoints()
    {
        foreach (var point in _spawnPoints.ToList())
        {
            if (Physics.CheckSphere(point, _radiusDetection, _baseMask))
            {
                _spawnPoints.Remove(point);
            }
        }
    }

    public Vector3 GetRandomPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Count)];
    }
}
