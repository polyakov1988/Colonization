using System;
using UnityEngine;

namespace Base
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private CollectorCollisionHandler _collisionHandler;
        
        private int _score;

        public event Action<int> ScoreIncrised;
            
        private void OnEnable()
        {
            _collisionHandler.CubeReceived += Increment;
        }

        private void OnDisable()
        {
            _collisionHandler.CubeReceived -= Increment;
        }

        private void Awake()
        {
            _score = 0;
            ScoreIncrised?.Invoke(_score);
        }

        private void Increment()
        {
            _score++;
            ScoreIncrised?.Invoke(_score);
        }
    }
}
