using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubePool _pool;
    [SerializeField] private SpawnPointGenerator _spawnPointGenerator;
    [SerializeField] private int _timeoutMin;
    [SerializeField] private int _timeoutMax;
    
    private bool _isActive;

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
            cube.Used += PutCubeIntoPool;
            cube.transform.position = _spawnPointGenerator.GetRandomPoint();
            cube.Init();
        }
    }

    private void PutCubeIntoPool(Cube cube)
    {
        cube.Used -= PutCubeIntoPool;
        _pool.PutCube(cube);
    }
}
