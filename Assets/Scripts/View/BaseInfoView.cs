using Base;
using UnityEngine;

namespace View
{
    public class BaseInfoView : MonoBehaviour
    {
        [SerializeField] private ScoreViewSpawner _scoreViewSpawner;
        
        public void AddScoreView(ScoreCounter scoreCounter, Color color)
        {
            ScoreView scoreView = _scoreViewSpawner.Get();
            scoreView.Init(scoreCounter, color);
            scoreView.transform.SetParent(transform);
            
        }
    }
}