using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeHandler : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private Dictionary<Cube, CubeStatus> _cubes;

    private void OnEnable()
    {
        _cubeSpawner.CubeSpawned += OnCubeSpawned;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeSpawned += OnCubeSpawned;
    }

    private void Awake()
    {
        _cubes = new Dictionary<Cube, CubeStatus>();
    }
    
    public bool CubeCanByReserved(Cube cube)
    {
        if (cube == null)
            throw new ArgumentNullException(nameof(cube));
        
        return _cubes[cube] == CubeStatus.Free;
    }
    
    public bool CubeCanByTaken(Cube cube)
    {
        if (cube == null)
            throw new ArgumentNullException(nameof(cube));
        
        return _cubes[cube] != CubeStatus.Moved;
    }

    public void ReserveCube(Cube cube)
    {
        if (cube == null)
            throw new ArgumentNullException(nameof(cube));
        
        _cubes[cube] = CubeStatus.Reserved;
    }
    
    public void CancelReserveCube(Cube cube)
    {
        if (cube == null)
            throw new ArgumentNullException(nameof(cube));
        
        _cubes[cube] = CubeStatus.Free;
    }

    private void OnCubeSpawned(Cube cube)
    {
        cube.Moved += OnCubeMoved;
        cube.Received += OnCubeReceived;
        
        _cubes.Add(cube, CubeStatus.Free);
    }
    
    private void OnCubeMoved(Cube cube)
    {
        _cubes[cube] = CubeStatus.Moved;
    }
    
    private void OnCubeReceived(Cube cube)
    {
        cube.Moved -= OnCubeMoved;
        cube.Received -= OnCubeReceived;

        _cubes.Remove(cube);
        
        _cubeSpawner.PutCubeIntoPool(cube);
    }
}
