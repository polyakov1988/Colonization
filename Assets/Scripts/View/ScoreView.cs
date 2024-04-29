using Base;
using TMPro;
using UnityEngine;

namespace View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private TMP_Text _text;

        private void OnEnable()
        {
            _scoreCounter.ScoreIncrised += ShowScore;
        }

        private void OnDestroy()
        {
            _scoreCounter.ScoreIncrised -= ShowScore;
        }

        private void ShowScore(int score)
        {
            _text.text = score.ToString();
        }
    }
}
