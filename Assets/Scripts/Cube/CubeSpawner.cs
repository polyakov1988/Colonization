using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubePool _pool;
    [SerializeField] private SpawnPointGenerator _spawnPointGenerator;
    [SerializeField] private int _timeoutMin;
    [SerializeField] private int _timeoutMax;
    
    private bool _isActive;

    public event Action<Cube> CubeSpawned;

    private void Awake()
    {
        _isActive = true;

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (_isActive)
        {
            yield return new WaitForSeconds(Random.Range(_timeoutMin, _timeoutMax));

            Cube cube = _pool.GetCube();
            cube.transform.position = _spawnPointGenerator.GetRandomPoint();
            CubeSpawned?.Invoke(cube);
        }
    }

    public void PutCubeIntoPool(Cube cube)
    {
        _pool.PutCube(cube);
    }
}