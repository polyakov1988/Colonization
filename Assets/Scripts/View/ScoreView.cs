using Base;
using TMPro;
using UnityEngine;

namespace View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private ScoreCounter _scoreCounter;

        public void Init(ScoreCounter scoreCounter, Color color)
        {
            _scoreCounter = scoreCounter;
            _scoreCounter.ScoreUpdated += ShowScore;
            _text.color = color;
        }

        private void OnDisable()
        {
            _scoreCounter.ScoreUpdated -= ShowScore;
        }
        
        

        private void ShowScore(int score)
        {
            _text.text = score.ToString();
        }
    }
}