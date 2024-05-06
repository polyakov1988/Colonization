namespace Base.Drone.StateMachine.State
{
    public class IdlingState : IState
    {
        private readonly Drone _drone;

        public IdlingState(Drone drone)
        {
            _drone = drone;
        }

        public void Enter()
        {
            _drone.SetFree();
        }

        public void Exit() { }
    }
}
