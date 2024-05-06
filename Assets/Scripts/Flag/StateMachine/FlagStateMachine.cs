using System.Collections.Generic;
using System.Linq;
using Flag.StateMachine.State;

namespace Flag.StateMachine
{
    public class FlagStateMachine : IStateSwitcher
    {
        private readonly List<IState> _states;
        private IState _currentState;

        public FlagStateMachine(Flag flag)
        {
            _states = new List<IState>()
            {
                new MovingState(flag),
                new StandingState(flag)
            };
        }

        public void SwitchState<T>() where T : IState
        {
            IState state = _states.FirstOrDefault(state => state is T);

            if (_currentState != null)
                _currentState.Exit();
            
            _currentState = state;
            _currentState.Enter();
        }
        
        public void Update()
        {
            _currentState.Update();
        }
        
        public void CleatState()
        {
            if (_currentState != null)
            {
                _currentState.Exit();
                _currentState = null;
            }
            
        }
    }
}
