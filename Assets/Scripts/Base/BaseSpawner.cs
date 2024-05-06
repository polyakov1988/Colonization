using System;
using System.Collections.Generic;
using Base.Drone;
using Flag;
using UnityEngine;

namespace Base
{
    public class BaseSpawner : MonoBehaviour
    {
        [SerializeField] private Base _basePrefab;
        [SerializeField] private DroneSpawner _droneSpawner;
        [SerializeField] private FlagSetter _flagSetter;
        [SerializeField] private int _rootBaseDroneCount;
        [SerializeField] private int _newBaseDroneCount;

        private int _maxBaseCount;
        private readonly Vector3 _rootPosition = Vector3.zero;
        private readonly Queue<Color> _colors = new ();

        public event Action<ScoreCounter, Color> BaseCreatedForView;
        public event Action BaseCreatedForPointGenerator;
        

        public void Init()
        {
            FillColors();
            _maxBaseCount = _colors.Count;
            Base rootBase = Instantiate(_basePrefab);
            rootBase.NotifyBaseSpawnerToBuildNewBase += BuildNewBase;

            Color color = _colors.Dequeue();
            
            rootBase.InitAsRoot(color, _rootPosition, _droneSpawner, _rootBaseDroneCount);
            BaseCreatedForView?.Invoke(rootBase.ScoreCounter, color);
            BaseCreatedForPointGenerator?.Invoke();
            rootBase.StartWork();
            
            _flagSetter.SubscribeToUsingFlag(rootBase.BaseFlagHandler);
        }

        private void FillColors()
        {
            _colors.Enqueue(Color.blue);
            _colors.Enqueue(Color.green);
            _colors.Enqueue(Color.red);
            _colors.Enqueue(Color.yellow);
            _colors.Enqueue(Color.magenta);
        }

        private void BuildNewBase(Drone.Drone drone, Base oldBase)
        {
            oldBase.NotifyBaseSpawnerToBuildNewBase -= BuildNewBase;
            
            Base newBase = Instantiate(_basePrefab);
            newBase.NotifyBaseSpawnerToBuildNewBase += BuildNewBase;

            Color color = _colors.Dequeue();

            Vector3 dronePosition = drone.transform.position;
            
            Vector3 newBasePosition = Vector3.zero;
            newBasePosition.x = dronePosition.x;
            newBasePosition.z = dronePosition.z;
            
            newBase.InitAsNew(color, newBasePosition, _droneSpawner, _newBaseDroneCount, drone);
            BaseCreatedForView?.Invoke(newBase.ScoreCounter, color);
            BaseCreatedForPointGenerator?.Invoke();
            newBase.StartWork();
            _flagSetter.SubscribeToUsingFlag(newBase.BaseFlagHandler);
        }
    }
}