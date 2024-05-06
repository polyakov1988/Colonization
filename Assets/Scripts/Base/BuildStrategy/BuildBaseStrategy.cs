using Base.Drone;
using UnityEngine;

namespace Base.BuildStrategy
{
    public class BuildBaseStrategy : BuildStrategy
    {
        private const int BasePrice = 2;

        private readonly DroneDispatcher _droneDispatcher;
        private readonly Transform _flagPosition;

        public BuildBaseStrategy(ScoreCounter scoreCounter, DroneDispatcher droneDispatcher, Transform flagPosition) : base(scoreCounter)
        {
            _droneDispatcher = droneDispatcher;
            _flagPosition = flagPosition;
        }

        protected override void OnScoreUpdated(int score)
        {
            if (score >= BasePrice)
            {
                _droneDispatcher.SendDroneToBuildNewBase(_flagPosition);
                Pay(BasePrice);
                Exit();
            }
        }
    }
}