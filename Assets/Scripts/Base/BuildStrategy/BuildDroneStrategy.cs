using Base.Drone;

namespace Base.BuildStrategy
{
    public class BuildDroneStrategy : BuildStrategy
    {
        private const int DronePrice = 1;

        private readonly DroneDispatcher _droneDispatcher;

        public BuildDroneStrategy(ScoreCounter scoreCounter, DroneDispatcher droneDispatcher) : base(scoreCounter)
        {
            _droneDispatcher = droneDispatcher;
        }

        protected override void OnScoreUpdated(int score)
        {
            if (score >= DronePrice)
            {
                _droneDispatcher.CreateDrone();
                Pay(DronePrice);
            }
        }
    }
}