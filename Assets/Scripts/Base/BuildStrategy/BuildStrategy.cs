namespace Base.BuildStrategy
{
    public abstract class BuildStrategy
    {
        private readonly ScoreCounter _scoreCounter;

        protected BuildStrategy(ScoreCounter scoreCounter)
        {
            _scoreCounter = scoreCounter;
        }

        public void Enter()
        {
            _scoreCounter.ScoreUpdated += OnScoreUpdated;
        }

        public void Exit()
        {
            _scoreCounter.ScoreUpdated -= OnScoreUpdated;
        }

        protected void Pay(int price)
        {
            _scoreCounter.Pay(price);
        }
        
        protected abstract void OnScoreUpdated(int score);
    }
}