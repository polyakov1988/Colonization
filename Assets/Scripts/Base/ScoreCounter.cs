using System;
using UnityEngine;

namespace Base
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private CollectorCollisionHandler _collisionHandler;
        
        private int _score;

        public event Action<int> ScoreUpdated;
            
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
            ScoreUpdated?.Invoke(_score);
        }

        public void Pay(int price)
        {
            _score -= price;
            ScoreUpdated?.Invoke(_score);
        }

        private void Increment()
        {
            _score++;
            ScoreUpdated?.Invoke(_score);
        }
    }
}
