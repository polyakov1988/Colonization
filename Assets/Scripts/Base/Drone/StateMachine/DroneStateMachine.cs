using System.Collections.Generic;
using System.Linq;
using Base.Drone.StateMachine.State;

namespace Base.Drone.StateMachine
{
    public class DroneStateMachine : IStateSwitcher
    {
        private readonly List<IState> _states;
        private IState _currentState;

        public DroneStateMachine(Drone drone)
        {
            _states = new List<IState>()
            {
                new IdlingState(drone),
                new MiningState(drone, this)
            };

            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<T>() where T : IState
        {
            IState state = _states.FirstOrDefault(state => state is T);

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }
}
