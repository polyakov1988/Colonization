using UnityEngine;

namespace View
{
    public class ScoreViewSpawner : MonoBehaviour
    {
        [SerializeField] private ScoreView _scoreViewPrefab;

        public ScoreView Get()
        {
            return Instantiate(_scoreViewPrefab);
        }
    }
}